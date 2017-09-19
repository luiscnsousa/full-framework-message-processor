# Simple message publishing and listening service

This service is a simple Windows Service that is intended to be deployed to [AWS](https://aws.amazon.com/).

The service will register an [SNS topic subscription](http://docs.aws.amazon.com/sns/latest/dg/welcome.html) using the amazing [JustSaying](https://github.com/justeat/JustSaying)
library. Once the service is started, it will publish a message and consume the message itself. The contents of the message will then be written to an S3 bucket.

Windows Service hosting is provided by [TopShelf](https://github.com/Topshelf/Topshelf/) and the HTTP endpoints for AWS health checks are served via [Nancy self-hosting](https://github.com/NancyFx/Nancy/wiki/Self-Hosting-Nancy).

This project intentionally uses the full .Net Framework so that it can be used as a comparison to the same service [written using .Net Core](TODO).
