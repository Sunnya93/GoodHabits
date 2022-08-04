// In the following line, you should include the prefixes of implementations you want to test.
window.indexedDB = window.indexedDB || window.mozIndexedDB || window.webkitIndexedDB || window.msIndexedDB;
// DON'T use "var indexedDB = …" if you're not in a function.
// Moreover, you may need references to some window.IDB* objects:
window.IDBTransaction = window.IDBTransaction || window.webkitIDBTransaction || window.msIDBTransaction || { READ_WRITE: "readwrite" }; // This line should only be needed if it is needed to support the object's constants for older browsers
window.IDBKeyRange = window.IDBKeyRange || window.webkitIDBKeyRange || window.msIDBKeyRange;
// (Mozilla has never prefixed these objects, so we don't need window.mozIDB*)


// This is what our customer data looks like.
const TodoData = [
    { Id: "1", Title: "test", Content: "contestntest", TodoDate: "2022-08-04", InsertDate: "2022-08-04", UpdateDate: "2022-08-04"  },
];


if (!window.indexedDB) {
    console.log("Your browser doesn't support a stable version of IndexedDB. Such and such feature will not be available.");
}

const dbName = "the_name";
let db;

export function CreateDatabase() {
    const request = indexedDB.open(dbName, 1);

    request.onerror = (event) => {
        console.log("Why didn't you allow my web app to use IndexedDB?!");
        console.error(`Database error: ${event.target.errorCode}`);
    };

    request.onsuccess = (event) => {
        db = event.target.result;
    };

    // This event is only implemented in recent browsers
    request.onupgradeneeded = (event) => {
        // Save the IDBDatabase interface
        const db = event.target.result;

        // Create an objectStore for this database
        const objectStore = db.createObjectStore("Todo", { keyPath: "Id" });
        objectStore.createIndex("Title", "Title", { unique: false });
        objectStore.createIndex("Content", "Content", { unique: false });
        objectStore.createIndex("TodoDate", "TodoDate", { unique: false });
        objectStore.createIndex("InsertTime", "InsertTime", { unique: false });
        objectStore.createIndex("UpdateTime", "UpdateTime", { unique: false });

        // Use transaction oncomplete to make sure the objectStore creation is
        // finished before adding data into it.
        objectStore.transaction.oncomplete = (event) => {
            // Store values in the newly created objectStore.
            const customerObjectStore = db.transaction("Todo", "readwrite").objectStore("Todo");
            customerData.forEach(function (customer) {
                customerObjectStore.add(customer);
            });
        };
    };
}

export function Add() {
    const transaction = db.transaction(["Todo"], "readwrite");
    // Note: Older experimental implementations use the deprecated constant IDBTransaction.READ_WRITE instead of "readwrite".
    // In case you want to support such an implementation, you can write:
    // var transaction = db.transaction(["customers"], IDBTransaction.READ_WRITE);

    // Do something when all the data is added to the database.
    transaction.oncomplete = (event) => {
        console.log("All done!");
    };

    transaction.onerror = (event) => {
        // Don't forget to handle errors!
    };

    const objectStore = transaction.objectStore("Todo");
    TodoData.forEach((Todo) => {
        const request = objectStore.add(Todo);
        request.onsuccess = (event) => {
            if (event.target.result === Todo.Id) {
                console.log("Success!");
            }
        };
    });
}

export function Delete() {
    const request = db.transaction(["Todo"], "readwrite")
        .objectStore("Todo")
        .Delete("1");
    request.onsuccess = (event) => {
        // It's gone!
        console.log("Delete Success");
    };
}

export function Get() {
    db.transaction("Todo").objectStore().get("1").onsuccess = (event) => {
        console.log(`Name for Key 1 is ${event.target.result.name}`);
    };
}

export function Update() {
    const objectStore = db.transaction(["Todo"], "readwrite").objectStore("Todo");
    const request = objectStore.get("1");
    request.onerror = (event) => {
        // Handle errors!
    };
    request.onsuccess = (event) => {
        // Get the old value that we want to update
        const data = event.target.result;

        // update the value(s) in the object that you want to change
        data.Content = "changed";

        // Put this updated object back into the database.
        const requestUpdate = objectStore.put(data);
        requestUpdate.onerror = (event) => {
            // Do something with the error
        };
        requestUpdate.onsuccess = (event) => {
            // Success - the data is updated!
        };
    };
}
