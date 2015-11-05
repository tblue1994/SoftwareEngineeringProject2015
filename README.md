# RAIK383H-Group-4

After cloning the repo, you will need to add the web config file and replace the placeholder localdb connection string with the proper remote connection string. The web config can be found <a href="https://dl.dropboxusercontent.com/u/100487576/Web.config">here</a>.

Next you will need to restore the missing Nuget packages.

Next, in the Demo.fs file in the RandomForestModel solution, change line 18 to the location of the activity data file on your computer

Also, In the app data folder, you will need to create the .mdf named "WebsiteContext-20150205215026.mdf" then in the nuget package manager, run the command update-database.
