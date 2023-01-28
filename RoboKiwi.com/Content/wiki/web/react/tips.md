---
title: React Tips
description: React Tips
guid: "38824191-9dfa-4b59-977b-1c42bd725217"
---

# Integrating dev server in Visual Studio

Extract the IIS Express certificate from the machine store under Personal certificates, to a pfx including the private key (set a password)

```
choco install openssl
openssl pkcs12 -in devserver.pfx -nocerts -out devserver.key
openssl pkcs12 -in devserver.pfx -clcerts -nokeys -out devserver.crt
openssl rsa -in devserver.key -out devserver-decrypted.key
```

* Add package reference to Microsoft.AspNetCore.SpaServices.Extensions
* Create a .env file in the root of the app folder:

```
BROWSER=none
SSL_CRT_FILE=devserver.crt
SSL_KEY_FILE=devserver-decrypted.key
```

* In Startup.cs, add the line `spa.UseProxyToSpaDevelopmentServer("https://localhost:3000");` (replacing existing line `spa.UseReactDevelopmentServer(npmScript: "start");`)
* In terminal launch the dev server with `npm run start`

# References

* https://www.freecodecamp.org/news/what-i-wish-i-knew-when-i-started-to-work-with-react-js-3ba36107fd13/
