---
title: Dynamic DNS
guid: "63ae34f9-e709-4c4a-8b9c-1f01068d6563"
---

## Dynamic DNS with Namecheap

### Setup in Namecheap

In the domain dashboard, go to the Advanced DNS panel, and add an `A+ Dynamic DNS Record`, entering the name for your dynamic host e.g. "homepc" and enter any dummy filter for the IP address e.g. `127.0.0.1`

Still within the Advanced DNS panel, scroll down and enable the Dynamic DNS. Make a copy of the `Dynamic DNS Password`.

### Setup script on the host PC

Create a batch file that will update your dynamic host IP address to the Namecheap DNS servers, automatically using the IP address the request comes from:

```cmd
@echo off
curl "https://dynamicdns.park-your-domain.com/update?host=HOSTNAME&domain=MYDOMAIN.COM&password=DYNAMICDNSPASSWORD"
```

### Schedule the script to run automatically

