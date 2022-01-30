---
title: Azure FrontDoor
guid: "54bc126a-ea18-48b2-a802-471b0d3fa4ae"
---

## Using custom certificates

If you want to use your own SSL certificates from KeyVault, you will need to create a service principal in your tenant that is associated with the globally-registered FrontDoor application id.

|SKU  | Application id  | Service principal name  |
|---------|---------|---------|
| Azure FrontDoor (Classic) | `ad0e1c7e-6d38-4ba4-9efd-0bc77ba9f037` | `Microsoft.Azure.Frontdoor` |
| FrontDoor Standard or Premium | `205478c0-bd83-4e1b-a9d6-db63a3e1e1c8` | `Microsoft.AzureFrontDoor-Cdn` |

### Frontdoor Classic

```bash
az ad sp create --id "ad0e1c7e-6d38-4ba4-9efd-0bc77ba9f037"
az role assignment create --assignee "ad0e1c7e-6d38-4ba4-9efd-0bc77ba9f037" --role "Contributor"
```

### Frontdoor Standard or Premium

```bash
az ad sp create --id "205478c0-bd83-4e1b-a9d6-db63a3e1e1c8"
az role assignment create --assignee "205478c0-bd83-4e1b-a9d6-db63a3e1e1c8" --role "Contributor"
```

Now you can go into Azure KeyVault and create an Access Policy to grant your FrontDoor Read access to both Secrets and Certificates.
