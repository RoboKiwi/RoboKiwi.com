---
title: HMAC
description: Hashed Message Authentication Code
guid: "201168a3-56f5-455d-989e-bb8704afd453"
---

## Overview

HMAC can be expanded as any variation of **h**ashed **m**essage **a**uthentication **c**ode,  keyed-**h**ash **m**essage **a**uthentication **c**ode or **h**ash-based **m**essage **a**uthentication **c**ode

### What HMAC does

* Verify the integrity of a message (that it hasn't been tampered with between the source and destination)
* Verify the authenticity of the message (that it came from a trusted sender)

### What HMAC does not do

* The message contents are **not** encrypted by the HMAC algorithm (though it's possible to choose to encrypt the message independent of HMAC)

## Further Reading

* [RFC2104](https://datatracker.ietf.org/doc/html/rfc2104)
