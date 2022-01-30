---
title: Application Security Groups (ASG)
guid: "39970bcd-096c-4412-b853-9979ee5234eb"
---

## Overview

Application Security Groups (ASG) let you "tag" resources.

For example, you could have a Quarantine tag that can assign a resource to a locked-down subnet / nsg until it can be secured.

ASGs are one of the options when choosing a source or destination on an NSG, allowing you to operate on resource tags rather than a service tag or address range.

At this stage, ASGs can only be applied to Virtual Machines to my knowledge.
