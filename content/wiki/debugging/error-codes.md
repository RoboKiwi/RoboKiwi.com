---
id: 392
title: Error Codes
date: 2010-12-17T10:38:15+00:00
description: Common error codes, causes and solutions.
parent: debugging
---

# Error Table

| Error Id                 | Error Text             | HRESULT (hex) | HRESULT (dec) | Description                                                                                                                                      | Solutions                                                                                                                                               | More Info |
|--------------------------|------------------------|---------------|---------------|--------------------------------------------------------------------------------------------------------------------------------------------------|---------------------------------------------------------------------------------------------------------------------------------------------------------|-----------|
| `E_FAIL`                 | `Unspecified error`    | `0x80004005`  | `-2147467259` |    |  |           |
| `REGDB_E_CLASSNOTREG`    | `Class not registered` | `0x80040154`  |               | Class not registered | The COM library is not registered. Find what DLL defines the class ID in the rest of the error message, and register it. |
|                          |                        | `0x800700c1`    |               | Mixing 32/64 problem? Compile to x86 instead of All Platforms for .NET assembly                                                                  |
| `E_OUTOFMEMORY`          |                        | `0x8007000e`    |
| `TYPE_E_CANTLOADLIBRARY` |                        | `0x80029c4a`    |               | The type library or DLL could not be loaded Type library not found, or maybe missing dependency loaded using LoadLibrary?                        |
|                          |                        | `0x800a005e`    |               | Invalid use of Null. Probably coming from a VB6 COM DLL, where a Null value is being used improperly (i.e., something is Null that shouldn't be) |
|                          |                        | `0x80070005`    |               |    | Permissions problem when registering DLL? Make sure you are in an elevated command prompt when trying to register (Administrator in the command window) |
| `TYPE_E_REGISTRYACCESS`  |                        | `0x8002801c`    |               | Error accessing the OLE registry                                                                                                                 |
|                          |                        | `0x900a005e`    |               | A VB6 result error code (more info needed)                                                                                                       |

## Tips

* When searching for the decimal code, be aware most search engines will exclude that term from search results because of the negative sign. Instead, surround the term with speech marks to prevent that: `"-2147467259"`.
* Try various searches for the hex code, decimal code and constant to find various results.
* Sometimes the error code will be from another platform or framework, for example, `900a005e` is from VB6.

## Example

`REGDB_E_CLASSNOTREG`

The error code will look like this in .NET: `0x80040154` (word) or `0xFFFFFFFF80040154` (dword), or a decimal value of `2147746132` (1 word) or `–2147221164` (dword)

Here's how it looks defined in `winerror.h`:

```cpp
#define REGDB_E_CLASSNOTREG _HRESULT_TYPEDEF_(0x80040154L)
```

## HRESULT

The `HRESULT` numbering space is vendor-extensible. Vendors can supply their own values for this field, as long as the C bit (`0x20000000`) is set, indicating it is a customer code.

The `HRESULT` numbering space has the following internal structure. Any protocol that uses `NTSTATUS` values on the wire is responsible for stating the order in which the bytes are placed on the wire.

* `S` (1 bit): Severity. If set, indicates a failure result. If clear, indicates a success result.
* `R` (1 bit): Reserved. If the N bit is clear, this bit MUST be set to `0`. If the N bit is set, this bit is defined by the `NTSTATUS` numbering space.
* `C` (1 bit): Customer. This bit specifies if the value is customer-defined or Microsoft-defined. The bit is set for customer-defined values and clear for Microsoft-defined values.
* `N` (1 bit): If set, indicates that the error code is an `NTSTATUS` value, except that this bit is set.
* `X` (1 bit):  Reserved.  SHOULD be set to `0`.
* `Facility` (11 bits): An indicator of the source of the error. New facilities are occasionally added by Microsoft.
* `Code` (16 bits)

## References

* [System Error Codes](https://docs.microsoft.com/windows/win32/debug/system-error-codes)
* [HRESULT docs](https://msdn.microsoft.com/library/cc231198.aspx)
* [List of HRESULT error codes](https://docs.microsoft.com/openspecs/windows_protocols/ms-erref/705fb797-2175-4a90-b5a3-3918024b10b8)
* [List of Win32 error codes](https://docs.microsoft.com/openspecs/windows_protocols/ms-erref/18d8fbe8-a967-4f1c-ae50-99ca8e491d2d)
