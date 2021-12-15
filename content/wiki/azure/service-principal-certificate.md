---
title: Authenticate a service principal using a self-signed certificate
---

## Create certificate

In Azure KeyVault, create a new self-signed certificate.

Export the certificate (pfx) and import to your machine's Personal key store.

Make note of the thumbprint.

## Authenticate to the correct tenant and subscription

```powershell
Connect-AzAccount -Tenant '<guid>' -SubscriptionId '<guid>'
```

## Create the service principal

```powershell
$cert = Get-Item cert:\CurrentUser\My\<thumbprint>
$keyValue = [System.Convert]::ToBase64String($cert.GetRawCertData())
$sp = New-AzADServicePrincipal -DisplayName DnsWriter -CertValue $keyValue -EndDate $cert.NotAfter -StartDate $cert.NotBefore
```

## Summary

Now you have a service principal set up that can authenticate with a certificate that is installed in your machine keystore.

You can now create scripts to interact with Azure automation without interaction.

Use RBAC to secure what is possible with the service principal.

```powershell
Connect-AzAccount -CertificateThumbprint '<certhumbprint>' -ApplicationId '<guid>' -Tenant '<guid>' -ServicePrincipal
```

## References & Further Reading

<https://docs.microsoft.com/azure/active-directory/develop/howto-authenticate-service-principal-powershell>