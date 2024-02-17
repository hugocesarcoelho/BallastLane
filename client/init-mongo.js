db.createUser(
    {
        user: "admin",
        pwd: "root",
        roles: [
            {
                role: "readWrite",
                db: "BallastLane"
            }
        ]
    }
);
db.createCollection("Application");
db.createCollection("User");