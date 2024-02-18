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

db.User.insert({
    "_id": "90700C24-1459-41AD-A16C-1A2756C7ADB0",
    "Name": "Admin",
    "Username": "admin",
    "Email": "admin@test.org",
    "Password": "7918a8c0f275b17862df8f6284864bc2abefb334da34d053e14b57f43bbf5b24c724faa07775999941932fdad4a86a1764913ad964fc91f3f8ce2fc3e3e1118f",
    "IsAdmin": true
});

db.User.insert({
    "_id": "195426df-9a07-4303-943f-d7cc269dc9f8",
    "Name": "John Matheus",
    "Username": "john",
    "Email": "john@test.org",
    "Password": "7918a8c0f275b17862df8f6284864bc2abefb334da34d053e14b57f43bbf5b24c724faa07775999941932fdad4a86a1764913ad964fc91f3f8ce2fc3e3e1118f",
    "IsAdmin": false
});

db.Application.insert({
    "_id": "4B52E32B-699A-4807-81BF-3F84A6B58E22",
    "Name": "Ballast Lane",
    "Description": "Ballast Lane Application",
});