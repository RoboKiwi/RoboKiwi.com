---
title: JavaScript Patterns
---
# JavaScript Patterns

## Generators

Rather than the new language feature generators, you can create your own generators in vanilla JavaScript:

```javascript
function element(array) {
    let i = 0;
    return function generator() {
        if( i < Array.length(array)) {
            let value = array[i];
            i += 1;
            return value;
        }
    }
}
```

## Promises

## References

https://www.html5rocks.com/en/tutorials/es6/promises/
https://www.infoq.com/news/2014/05/promises-javascript
