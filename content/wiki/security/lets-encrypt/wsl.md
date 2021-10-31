---
title: Let's Encrypt Certbot on WSL
description: Let's Encrypt using Certbot on Windows Subsystem for Linux (WSL)
toc: true
menu:
    wiki:
        identifier: "certbot-wsl"
        parent: "lets-encrypt"
---

## Prerequisites

* Set up [Ubuntu on Windows Subsystem for Linux (WSL)]({{<relref "/wiki/windows/wsl.md">}})

## Get Started

Open a new Ubuntu shell in Windows Terminal.

### Install Certbot

Snap currently isn't working properly in WSL2 though it's the recommended installation method for Certbot: `sudo snap install --classic certbot`

Instead, we will use Python's PIP using the instructions @ [Certbot install via pip](https://certbot.eff.org/lets-encrypt/pip-other)

```bash
# Get updated list of packages
sudo apt update

# Install Python3 and some dependencies
sudo apt install python3 python3-venv libaugeas0

# Setup virtual Python environment for Certbot
sudo python3 -m venv /opt/certbot/

# Upgrade pip
sudo /opt/certbot/bin/pip install --upgrade pip

# Install Certbot
sudo /opt/certbot/bin/pip install certbot

# Link /usr/bin/certbot -> /opt/certbot/bin/certbot
sudo ln -s /opt/certbot/bin/certbot /usr/bin/certbot
```

Now we can play with certbot!

## Generating a certificate against Azure DNS

We will request a certificate for a domain we have in Azure DNS.

Create a service principal, and then set up some variables:

```bash
AZURE_CLIENTID=00000000-0000-0000-0000-000000000000
AZURE_CLIENTSECRET=<service principal password>
AZURE_TENANT=00000000-0000-0000-0000-000000000000 or <tenant>.onmicrosoft.com
AZURE_RESOURCEGROUP=<resource group name>
AZURE_DNSZONE=<dns zone>
AZURE_CERTSECRET=<password to give the PFX>
AZURE_KEYVAULT=<name of Azure KeyVault for storing cert>
```

Now we can request a test certificate using DNS record validation:

```bash
sudo certbot certonly --manual --preferred-challenges dns -d "$AZURE_DNSNAME.$AZURE_DNSZONE" \
    --register-unsafely-without-email \
    --agree-tos \
    --test-cert \
    --manual-auth-hook "./validate.sh" \
    --manual-cleanup-hook "./cleanup.sh" \
    --deploy-hook "./deploy.sh" \
    --disable-hook-validation \
    --logs-dir "/opt/certbot/log" \
    --work-dir "/opt/certbot/lib" \
    --config-dir "/opt/certbot/letsencrypt"
```

You should see a response stating the validation value and prompting you to hit `ENTER` to continue:

```txt
Please deploy a DNS TXT record under the name:

_acme-challenge.<your domain>.

with the following value:

<validation key value>
```

Keep this terminal open and don't hit ENTER yet; open another terminal and set the variables you need again.

Now we can set the validation variables. The challenge will always be `_acme-challenge` without the domain suffix, as that is always implied from the DNS zone we're updating in Azure DNS.

Set the `CERTBOT_VALIDATION` variable to the value that certbot prompted you with.

```bash
CERTBOT_CHALLENGE=_acme-challenge
CERTBOT_VALIDATION=<validation key value>
```

Now we can set the DNS challenge record in Azure DNS.

```bash
# Authenticate to Azure
az login --service-principal -u $AZURE_CLIENTID -p $AZURE_CLIENTSECRET --tenant $AZURE_TENANT

# Set the ACME DNS Validation challenge TXT record
az network dns record-set txt add-record -g $AZURE_RESOURCEGROUP -z $AZURE_DNSZONE -n $CERTBOT_CHALLENGE -v $CERTBOT_VALIDATION

```

Now you can go back to your certbot tab and hit `ENTER` so that the Let's Encrypt servers can validate against our DNS record. Give it a bit of time, then you should receive your certificate:

```bash
Successfully received certificate.
Certificate is saved at: /etc/letsencrypt/live/<domain>/fullchain.pem
Key is saved at:         /etc/letsencrypt/live/<domain>/privkey.pem
This certificate expires on <date>.
These files will be updated when the certificate renews.
```

Going back to our other terminal, let's clean up our validation record:

```bash
az network dns record-set txt remove-record -g $AZURE_RESOURCEGROUP -z $AZURE_DNSZONE -n "_acme-challenge" -v $CERTBOT_VALIDATION
```

Then back to our certbot terminal, we can create our PFX that contains our certificate, the chain and the private key:

```bash
# Convert to .pfx
sudo openssl pkcs12 -export -out $RENEWED_LINEAGE/pkcs12_cert.pfx -inkey $RENEWED_LINEAGE/privkey.pem -in $RENEWED_LINEAGE/cert.pem -certfile $RENEWED_LINEAGE/chain.pem -password pass:$AZURE_CERTSECRET
```

And now we can import that key to Azure KeyVault for safe keeping:

TODO: Replace dots in the domain with hyphens

```bash
sudo az keyvault certificate import --vault-name $AZURE_KEYVAULT -n $CERTBOT_DOMAIN -f $RENEWED_LINEAGE/pkcs12_cert.pfx --password $AZURE_CERTSECRET
```
