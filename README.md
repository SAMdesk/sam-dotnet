SAM .NET
==========

## Overview

This repo contains a .NET middleware to simplify the usage of the SAM API.

## Installation

Binaries are available in the bin folder (see SAM.dll).

## Documentation

Up to date API Documentation can be found [here](https://api.samdesk.io).

## API Overview

Every resource is accessed via a SamClient instance:

```c#
SamClient client = new SamClient(AuthType.API_KEY, "<API_KEY>");
// Account account = client.RetrieveAccount();
```

### Authentication

In order to use the SAM API, you must provide the SamClient instance with an API key. There are 3 ways to do this:

1) Pass it in the constructor:

```c#
SamClient client = new SamClient(AuthType.API_KEY, "<API_KEY>");
```

2) Call setAuth on an existing SamClient instance:

```c#
SamClient client = new SamClient();
client.setAuth(AuthType.API_KEY, "<API_KEY>");
```

3) Pass a SamAuth object as the auth parameter to a request method:

```c#
SamClient client = new SamClient();
client.RetrieveAccount(new SamAuth(AuthType.API_KEY, "<API_KEY>"));
```

### Available resources & methods

 * Account
  * `RetrieveAccount([auth])`
  * `ListAccountUsers([auth])`
 * Assets
  * `ListAssets(storyId[, parameters, auth])`
  * `RetrieveAsset(storyId, assetId[, parameters, auth])`
  * `CreateAsset(storyId, parameters[, auth])`
 * Stories
  * `ListStories([parameters, auth])`
  * `RetrieveStory(storyId[, parameters, auth])`
  * `CreateStory(parameters[, auth])`
  * `DeleteStory(storyId[, auth])`
 * Upload
  * `UploadMedia(bytes, mimetype[, name, auth])`
  * `StartUpload(parameters[, auth])`
  * `AppendUpload(parameters[, auth])`
  * `CompleteUpload(mediaId[, auth])`
 * User
  * `RetrieveUser([auth])`

## Author

Officially maintained by SAM.