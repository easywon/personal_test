personal_cdc_test

5/10/19 -
    Starting work on creating a basic program to connect and communicate with server cougar5

5/11/19 - 5/14/19
    Created a basic CDC functionality to communicate between C# and Snowflake.

5/15/19 -
    Begun implementation of transactions and logging for CDC.
    Ran into issues with session continuity. Changing the scope of the transaction from Snowflake to .NET.
    The transaction will be constrained to be inside C# only.

    3:04 PM - The method of constraining transactions to be inside C# only worked well.
              Commits, rollbacks, and session management works as desired.

5/16/19
    9:38 AM - Starting to outline basic logging process.
    12:00 PM - Managed to create an internal transaction to handle the insertion of new log items.
    2:30 PM - Created both start and end log methods to mark when the database is being accessed and changed.
    5:00 PM - Restructured the logging table and logging methods to match it.
