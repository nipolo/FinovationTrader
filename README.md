# FinovationTrader app
### Instructions to run the application

1. Clone the repo
2. Install docker for Windows (verify that docker-compose is working)
3. Open the solution FinovationTrader.sln with Visual Studio 2019 
4. Change startup project to be docker-compose in Deployment 
5. Build and run the solution 
6. Verify that the database is up - create connection using DBeaver or SQL Management Studio:
    - DB server - localhost:1433
    - Database - master
    - user name - sa
    - password 1qaz!QAZ
7. Import Postman requests to the local desktop app (can be found in the phisical direcotry ~path~of~the~clonned~repo/Documentation/Finovation.postman_collection.json)
8. Test all the required usecases 
