---
title: Overview of .NET configuration
---

## Overview

The .NET configuration extensions are really just a cascading set of properties that are loaded from any number of sources, in increasing priority.

There are several built-in configuration providers.

When configuration is loaded through the providers, they are all serialized to a dictionary, with keys denoting the setting name and hierarchy using ":" as the delimiter.

## Load order
