# Keybase F\#

This is primarily an F# wrapper around the [keybase](martindevans@gmail.com) API.

Documentation for Keybase can be found [here](https://keybase.io/docs). Specifically API docs can be found [here](https://keybase.io/docs/api/1.0)

### Angeronia

Angeronia is a C# app which uses the F# library to access Keybase. It's intended provide all the access you need to keybase services without you ever needing to access the keybase website.

### Keybase

This is the wrapper around the Keybase API. The library should be written to be as paranoid as possible about keybase - every response should be cross checked with other sources of truth.

### Keybase.Test

This is a simple test harness for the Keybase project.