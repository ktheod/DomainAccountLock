# DomainAccountLock
Based on MS ALTools LockoutStatus application.

This application runs as a task icon and can be triggered either on demand or on a timer.
It executes an Powershell script to ping the Domain Controllers and get same information as the LockoutStatus app. Results are saved in a CSV file for further processing but also for user visibility.

It periodically checks all or configured Domain Controllers for a specific domain username and updates the tray icon with the current Bad Pwd Count displayed as a number, so that you don't have to check the CSV results file for the details. 

If the count is above 3 it will also play a beep sound (sounds can be disabled from settings), then again above 5 and lastly above 7.

Usually locking the computer and then logging in again, will zero the counter in the affected Domain Controllers.

Hopefully helpful for periods of frequent domain account lockouts and during debuging.
