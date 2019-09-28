Echo Restoring nuget packets
nuget restore
Echo Building solution
msbuild NumberSorter.sln
Echo Testing solution 
dotnet test NumberSorter.sln