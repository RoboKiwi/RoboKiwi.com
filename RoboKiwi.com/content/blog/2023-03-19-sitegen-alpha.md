---
title: SiteGen .NET static website generator
date: 2023-01-29T20:00:00+12:00
categories:
- development
tags:
- .net
- jamstack
- markdown
- sitegen
author: David
description: A new static website generator in .NET
url: /blog/2023/01/29/sitegen/
---
## Overview

I've made a start on a static website generator written in .NET. This website is now built on it.

## Why another static website generator?

While impressively fast, I found the templating features and style of Hugo, and its lack of extensibility made me start looking elsewhere.

Other static website generators tend to lean too heavily into Node, NPM and very opinionated front-end frameworks.

When it comes to templating the site, and in particular dealing with things like hierarchical navigation and table of contents, the former also seemed unwieldy.

I decided to see how fast I could get a pure .NET implementation going first, and if I could port my blog across from Hugo.

Using ASP.NET MVC and Razor templating made it easy to rapidly build out the website, particularly with things like the hierarchy traversal and
navigation which was tricky to unviable in Hugo. And F5 to debug and iterate is great here.

## What's working so far

- Markdown rendering
- Front matter support for YAML, TOML and JSON
- Git integration for author, date and time for creation and modification
- Generate static resources for Mermaid diagrams to ensure the site is still fully static
- Syntax highlighting through Pygments
- Fast, easy generation and traversal of the site hierarchy to generate the site's navigation through patterns 
  like breadcrumbs, nested navigation, linking to siblings, descendents and ancestors etc
- Built-in taxonomies including categories and tags
- Site map generation
- Generating from **any** source that can provide a site map
- GH Actions building and deployment for Azure Static WebApps

## Next features

I'm currently working on a few important features:

- Rich pagination
- Internationalization (i18n) and Localization (l10n) support
- Outputs to other formats, starting with RSS, Atom, JSON
- Search engine integration such as Algolia
- Static comments engine integration
- Migrating from other site generators and CMSs (e.g. Wordpress, Hugo)
- Optimization passes
