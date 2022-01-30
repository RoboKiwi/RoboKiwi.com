---
title: Polyfills
guid: "a0e95a15-7099-4ac6-b389-cf6c5a74198c"
---

## Object.create

```javascript
if (typeof Object.create != 'function') {
  Object.create = (function() {
    var Temp = function() {};
    return function (prototype) {
      if (arguments.length > 1) {
        throw Error('Second argument not supported');
      }
      if (typeof prototype != 'object') {
        throw TypeError('Argument must be an object');
      }
      Temp.prototype = prototype;
      var result = new Temp();
      Temp.prototype = null;
      return result;
    };
  })();
}
```

[MDN Reference](https://developer.mozilla.org/docs/Web/JavaScript/Reference/Global_Objects/Object/create)

## Inheritance

```javascript
var ctor = function (options) {
    options = options || {};

    options.type = "user";
    options.factory = user;
    options.allowRemove = true;
    options.removeDialog = 'user/remove';
    options.view = 'user/editUser.html';

    // Base constructor
    editBase.call(this, options);
    
    this.defaultRoles = options.defaultRoles || [];    
};

// Hook up inheritance
ctor.prototype = Object.create(editBase.prototype);
ctor.prototype.constructor = ctor;

```
