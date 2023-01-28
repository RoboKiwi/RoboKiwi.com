---
title: SVG
guid: "773732b2-8df4-4e28-8f27-7388e4408361"
---

# SVG Sprites

You can define your SVG sprites once in the page:

```html
<svg xmlns="http://www.w3.org/2000/svg" style="display: none;">
  
  <!-- Replace <svg> tag with <symbol> tag. You can keep the viewBox here too. -->
  <!-- Assign the symbol an id you can use to refer to it later -->
  <symbol id="mySvgSprite1" viewBox="100 10 80 300">
   <!-- You can add title and desc tags for accessibility -->
    <title>My SVG Sprite</title>
    <desc>Detailed description of my sprite</desc>
    <!-- SVG source -->
    <path>...</path>  
  </symbol>
  
  <symbol id="mySvgSprite2" viewBox="0 20 50 60">
    <!-- SVG source --> 
  </symbol>
  
</svg>
```

Now you can re-use the sprites in the page:

```html
<svg><use xlink:href="#mySvgSprite1" /></svg>
```
