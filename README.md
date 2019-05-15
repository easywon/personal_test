personal_cdc_test

5/10/19 -
    Starting work on creating a basic program to connect and communicate with server cougar5

5/11/19 - 5/14/19
    Created a basic CDC functionality to communicate between C# and Snowflake.

5/15/19 -
    Begun implementation of transactions and logging for CDC.
    Ran into issues with session continuity. Changing the scope of the transaction from Snowflake to .NET.
    The transaction will be constrained to be inside C# only.

