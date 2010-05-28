Kokugen
=======

Description
-----------

Kokugen is a .Net based Project and Time Tracker built with [FubuMVC](http://fubumvc.com/). Once finished Kokugen will be able to track Project work items, employees, basic company information, and the time employees spend on projects and work items. Once the API is complete there will also be desktop widgets for Windows 7 and Mac OS X for employees to use for time tracking.

TO DO:
------

(Minor stuff:)
* Style Company List
* Finish Styling Project List
* Move Project url's to match the style we have on Company
* Add Employee/User management
* Work Item Management

* Possible Active Directory Auth plugin?

How To Build
------------

Grab the sources from here on github. 

Go to your console and type the following commands:

1. rake dbCreate
2. rake

(The order of these might vary)

The first command will setup the KokugenData database in your local SQL Server. Most of the developers are not using SQLExpress, so you may have to modify your connection settings in the Rakefile.rb (DO NOT COMMIT THOSE CHANGES)

The second command will compile the app and run tests.
