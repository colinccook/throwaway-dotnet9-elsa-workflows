# throwaway-dotnet9-elsa-workflows

I'm experimenting with encapsulating a saga in C# code.

This saga is a mini-saga that happens as part of a longer running process of onboarding a broadband customer.

As part of the onboarding process, the "broadband package configuration" of the onboarding may differ from what they currently have (currently on 100mb, customer has signed up to 1gb).

This saga encapsulates sending a message to a third party (OpenReach) to request they make the physical change.
