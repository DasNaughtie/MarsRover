Opted to create just the domain and application layers, making use of tests to prove my concept.

Front end would in theory by wired up with a simple compostion root or .net core built-in DI.  Could accept a text file to support the input format.

The tests are an example...not exhaustive.  As such I have focused on the main 'sunny day' scenario where the application service test is concerned based on my understanding of the 
requirements.

Layout I have opted for is as described in 'Implementing Domain Driven Design' by Vaughn Vernon.

Interfaces are in the class file - some people hate this but I find it keeps the number of files down.  No preference personally but as it's a simple solution opted to do this.

Think that's it!