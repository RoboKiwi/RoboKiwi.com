---
title: Let's Encrypt for Azure DNS
description: Create free SSL certificates for Azure DNS domains using Let's Encrypt
categories:
- azure
---

## Steps

* Create a Service Principal in Azure for the automation to use
* Set secrets on GitHub Repository
* Configure GitHub Actions Workflow

## Create Service Principal

Install the Azure CLI if you haven't already.

Log in to the subscription and tenant that contains the Azure DNS zone.

```bash
az login
```

This will echo out a bunch of JSON for all the subscriptions; make note of the subscription id and tenant id you want to use.

```bash
# Set this to the id of the subscription you're going to be operating within
subscriptionId=<guid>

# Set the subscription as your default
az account set -s $subscriptionId

# Confirm that the subscription is now set as your default
az account show
```

### Create role

First we'll create the role to give the service principal we will create access to DNS TXT records.

We'll base it off the permissions for the built-in [DNS Contributor](https://docs.microsoft.com/azure/role-based-access-control/built-in-roles#dns-zone-contributor) role.

```bash
az role definition create --role-definition '{
    "Name": "DNS TXT Record Contributor",
    "Description": "Lets you manage TXT record sets in Azure DNS zones, but does not let you control who has access to them.",
    "Actions": [
        "Microsoft.Authorization/*/read",
        "Microsoft.Insights/alertRules/*",
        "Microsoft.Network/dnsZones/TXT/*",
        "Microsoft.Network/dnsZones/read",
        "Microsoft.ResourceHealth/availabilityStatuses/read",
        "Microsoft.Resources/deployments/*",
        "Microsoft.Resources/subscriptions/resourceGroups/read"
    ],
    "DataActions": [],
    "NotDataActions": [],
    "AssignableScopes": ["/subscriptions/<guid>"]
}'
```

### Create service principal

```bash
tenantId=<guid>
az ad sp create-for-rbac --name AcmeBot --role "DNS TXT Record Contributor" --scopes /subscriptions/$subscriptionId
```

Now give the service principal access to import certificates:

```bash
az role assignment create --assignee AcmeBot --role "Key Vault Certificates Officer" --scope /subscriptions/<guid>
```

The output will contain your password, which you must take note of as you won't be able to retrieve it later.

I recommend you add the `appId` and the `password` to Azure KeyVault and set the expiry date to a year from now.

```json
{
  "appId": "<guid>",
  "displayName": "AcmeBot",
  "name": "<guid>",
  "password": "<pwd>",
  "tenant": "<guid>"
}
```

If you go to the Azure portal, you'll see a new application registration for this Service Principal under All applications: <https://portal.azure.com/#blade/Microsoft_AAD_IAM/ActiveDirectoryMenuBlade/RegisteredApps>

### Test manually adding a TXT record

Log in using the new service principal

```bash
az login --service-principal -u "<guid>" -p "<pwd>" --tenant "<guid>"
```

```bash
az network dns record-set txt add-record -g <resourceGroupName> -z <dnsZoneName> -n "<subdomain>" -v "<Test value>"
```

You should get a recordset back:

```json
{
  "etag": "<guid>",
  "fqdn": "subdomain.yourdomain.com.",
  "id": "/subscriptions/<guid>/resourceGroups/<resourceGroupName>/providers/Microsoft.Network/dnszones/<dnsZoneName>/TXT/<subdomain>",
  "metadata": null,
  "name": "<subdomain>",
  "provisioningState": "Succeeded",
  "resourceGroup": "<resourceGroupName>",
  "targetResource": {
    "id": null
  },
  "ttl": 3600,
  "txtRecords": [
    {
      "value": [
        "Testing"
      ]
    }
  ],
  "type": "Microsoft.Network/dnszones/TXT"
}
```

Clean up after yourself by deleting the test record:

```bash
az network dns record-set txt remove-record -g <resourceGroupName> -z <dnsZoneName> -n "<subdomain>" --value "<Test value>"
```

## Certbot

`certbot` (formerly `letsencrypt`) is the official ACME implementation originally from Let's Encrypt, now maintained by the Electronic Frontier Foundation (EFF), one of the founders of Let's Encrypt.

### Why Certbot?

As Certbot is effectively the official implementation of the ACME protocol, it's preferable to use it over some of the available community alternatives that may be more specific or specialized to a particular language or platform but may lag behind the protocol, support and security.

## Certbot from Docker

Executing Certbot in Docker, we can run it on any platform including Windows, and store the acquired certificate in Azure KeyVault to be acquired and used by Azure services or our own machines.

For our purposes we will extend the Docker image to include the Azure CLI, and use the DNS TXT record validation hooks to validate against our Azure DNS Zone.

These are the key command-line options we will use to interact with certbot:

| Option            | Description                                                                                                    |
|-------------------|----------------------------------------------------------------------------------------------------------------|
| `certonly`        | Obtain or renew a certificate, but do not install it                                                           |
| `-d DOMAINS`      | Comma-separated list of domains to obtain a certificate for                                                    |
| `--manual`        | Obtain certificates interactively, or using shell script hooks                                                 |
| `-n`              | Run non-interactively                                                                                          |
| `--test-cert`     | Obtain a test certificate from a staging server                                                                |
| `--dry-run`       | Test "renew" or "certonly" without saving any certificates to disk                                             |
| `-v`, `--verbose` | This flag can be used multiple times to incrementally increase the verbosity of output, e.g. -vvv. (default:0) |
| `--agree-tos`     | Agree to the ACME Subscriber Agreement (default: Ask) |

```bash
#!/bin/bash
# validate.sh

# The full name for the TXT record
CERTBOT_CHALLENGE="_acme-challenge.$CERTBOT_DOMAIN"

# Authenticate to Azure
az login --service-principal -u $AZURE_CLIENTID -p $AZURE_CLIENTSECRET --tenant $AZURE_TENANT

# Set the ACME DNS Validation challenge TXT record
az network dns record-set txt add-record -g $AZURE_RESOURCEGROUP -z $AZURE_DNSZONE -n $CERTBOT_CHALLENGE -v $CERTBOT_VALIDATION

# Give some time for DNS propagation
sleep 20
```

```bash
#!/bin/bash
# validate.sh
# Authenticate to Azure
#az login --service-principal -u "$AZURE_CLIENTID" -p "$AZURE_CLIENTSECRET" --tenant "$AZURE_TENANT"
source $GITHUB_ENV
export

# Get the name of the first resource group that contains the DNS Zone
# Note we have to trim carriage return from the result, see <https://github.com/Azure/azure-cli/issues/8348>
AZURE_RESOURCEGROUP=$(az network dns zone list --output tsv --query "[?name=='$AZURE_DNSZONE'] | [0].resourceGroup" | tr -d '\r')

# Set the ACME DNS Validation challenge TXT record
az network dns record-set txt add-record -g "$AZURE_RESOURCEGROUP" -z "$AZURE_DNSZONE" -n "_acme-challenge.$AZURE_DNSNAME" -v "$CERTBOT_VALIDATION"

# Give it some time to propagate
sleep 20
```

```bash
#!/bin/bash
# cleanup.sh
source $GITHUB_ENV

AZURE_RESOURCEGROUP=$(az network dns zone list --output tsv --query "[?name=='$AZURE_DNSZONE'] | [0].resourceGroup" | tr -d '\r')
az network dns record-set txt remove-record -g "$AZURE_RESOURCEGROUP" -z "$AZURE_DNSZONE" -n "_acme-challenge.$AZURE_DNSNAME" -v "$CERTBOT_VALIDATION"
```

```bash
#!/bin/bash
# deploy.sh
source $GITHUB_ENV

# Convert to .pfx
openssl pkcs12 -export -out "$RENEWED_LINEAGE/pkcs12_cert.pfx" -inkey "$RENEWED_LINEAGE/privkey.pem" -in "$RENEWED_LINEAGE/cert.pem" -certfile "$RENEWED_LINEAGE/chain.pem" -password pass:"$AZURE_CERTSECRET"

# KeyVault only allows alphanumeric and dashes / hyphens
AZURE_CERTNAME=CertBot-$(echo $CERTBOT_DOMAIN | tr -s [:punct:][:blank:] "-" | tr -d '\r')

echo "AZURE_CERTNAME=$AZURE_CERTNAME"

# Import to Azure KeyVault
az keyvault certificate import --vault-name "$AZURE_KEYVAULT" -n "$AZURE_CERTNAME" -f "$RENEWED_LINEAGE/pkcs12_cert.pfx" --password "$AZURE_CERTSECRET" --tags "authority=certbot"
```

```dockerfile
FROM certbot/certbot:latest

# Install Azure CLI
RUN \
  apk update && \
  apk add bash py-pip && \
  apk add --virtual=build gcc libffi-dev musl-dev openssl-dev python-dev make && \
  pip --no-cache-dir install -U pip && \
  pip --no-cache-dir install azure-cli && \
  apk del --purge build

# Copy our hook scripts for validation and cleanup
COPY ./*.sh /var/scripts
```

When we run the docker container, we must pass our secrets through as environment variables:

| Variable            | Description |
|---------------------|-------------|
| AZURE_CLIENTID      |             |
| AZURE_CLIENTSECRET  |             |
| AZURE_TENANT        |             |
| AZURE_RESOURCEGROUP |             |
| AZURE_DNSZONE       |             |
| AZURE_CERTSECRET     | Password for the generated PFX file |

```bash
docker run -it --rm --name certbot \
 -e AZURE_CLIENTID="$AZURE_CLIENTID" \
 -e AZURE_CLIENTSECRET="$AZURE_CLIENTSECRET" \
 -e AZURE_TENANT="$AZURE_TENANT" \
 -e AZURE_RESOURCEGROUP="$AZURE_RESOURCEGROUP" \
 -e AZURE_DNSZONE="$AZURE_DNSZONE" \
 certbot/certbot certonly --manual --preferred-challenges dns \
--manual-auth-hook "/var/scripts/validate.sh" \
--manual-cleanup-hook "/var/scripts/cleanup.sh" \
--deploy-hook "/var/scripts/deploy.sh" \
--disable-hook-validation \
```

## References & Further Reading

* [Official az ad sp create-for-rbac documentation](https://docs.microsoft.com/cli/azure/ad/sp?view=azure-cli-latest#az_ad_sp_create_for_rbac)
* [Official certbot --manual documentation](https://certbot.eff.org/docs/using.html#manual)
* [Official certbot validation hooks documentation](https://certbot.eff.org/docs/using.html#pre-and-post-validation-hooks)
