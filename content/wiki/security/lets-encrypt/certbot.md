---
title: Certbot
description: The 'official' ACME client Certbot
menu:
    wiki:
        parent: "lets-encrypt"
        weight: 10
---

## Overview

Certbot is considered the official implementation of the Automated Certificate Management Environment (ACME) protocol, to be used for acquiring, renewing and installing certificates.

ACME uses a challenge / response protocol to establish ownership of the domain that a certificate is being issued for; either dynamically through HTTP or by verifying through DNS TXT records when the web server isn't accessible, or when issuing wildcard certificates.

## References & Further Reading

* [Certbot Website](https://certbot.eff.org/)
* [Certbot Docs](https://certbot.eff.org/docs/)
