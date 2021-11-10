---
title: OSI Model
description: Open Systems Interconnection model
---

## Overview

The Open Systems Interconnection model (OSI model) defines seven layers that abstract the flow of data through computer & telecommunications systems.

| Layer | Name         | Protocol          | TCP/IP          | Examples                | Description                                                                                                                                      |
|-------|--------------|-------------------|-----------------|-------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------|
| 10    | Government   |                   |                 |                         | According to Bruce Schneider & RSA                                                                                                               |
| 9     | Organization |                   |                 |                         | According to Bruce Schneider & RSA                                                                                                               |
| 8     | User         |                   |                 |                         | According to Bruce Schneider & RSA                                                                                                               |
| 7     | Application  | Data              | HTTP, HTTPS     | WebRTC, WebSocket       | High-level APIs, including resource sharing, remote file access                                                                                  |
| 6     | Presentation | Data              | TLS/SSL, MIME   | ASCII, MPEG             | Translation of data between a networking service and an application; including character encoding, data compression and encryption/decryption    |
| 5     | Session      | Data              | Sockets         | SOCKS, Named pipes, RPC | Managing communication sessions, i.e., continuous exchange of information in the form of multiple back-and-forth transmissions between two nodes |
| 4     | Transport    | Segment, Datagram | TCP, UDP        |                         | Reliable transmission of data segments between points on a network, including segmentation, acknowledgement and multiplexing                     |
| 3     | Network      | Packet            | IP, IPSec, ICMP |                         | Structuring and managing a multi-node network, including addressing, routing and traffic control                                                 |
| 2     | Data link    | Frame             | PPP             |                         | Reliable transmission of data frames between two nodes connected by a physical layer                                                             |
| 1     | Physical     | Bit, Symbol       |                 |                         | Transmission and reception of raw bit streams over a physical medium                                                                             |medium                                                                             |

## References

* [OSI model](https://en.wikipedia.org/wiki/OSI_model)
* [Layer 8](https://en.wikipedia.org/wiki/Layer_8)
