---
title: Let's Encrypt
description: Acquiring and renewing free SSL certificates from Let's Encrypt
menu:
    wiki:
        parent: "security"
        identifier: "lets-encrypt"
        weight: 10
---

## Overview

All HTTP traffic should encrypted end-to-end using SSL/TLS.

Let's Encrypt makes this possible by offering free SSL certificates to any domain you can prove ownership of.

Let's Encrypt uses the ACME protocol for automating the certificate management workflow. Their server-side implementation Boulder is written in Python,
while the defacto default ACME client is Certbot, written in Python.

Certificates issued by Let's Encrypt last for 90 days, with the recommendation to renew your certificates every 60 days.
