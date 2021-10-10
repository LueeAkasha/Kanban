BEGIN TRANSACTION;
CREATE TABLE IF NOT EXISTS "boards" (
	"Email"	TEXT NOT NULL,
	"BoardId"	INTEGER NOT NULL,
	"BoardCreator"	TEXT,
	PRIMARY KEY("Email")
);
CREATE TABLE IF NOT EXISTS "columns" (
	"ColumnName"	TEXT NOT NULL,
	"ColumnOrdinal"	INTEGER NOT NULL,
	"Limit"	INTEGER NOT NULL,
	"BoardId"	INTEGER NOT NULL,
	PRIMARY KEY("BoardId","ColumnName")
);
CREATE TABLE IF NOT EXISTS "tasks" (
	"EmailAssignee"	TEXT NOT NULL,
	"Title"	TEXT NOT NULL,
	"Description"	TEXT,
	"DueDate"	TEXT NOT NULL,
	"CreationTime"	TEXT NOT NULL,
	"taskId"	INTEGER NOT NULL,
	"ColumnName"	TEXT NOT NULL,
	"BoardId"	INTEGER NOT NULL,
	PRIMARY KEY("taskId","BoardId")
);
CREATE TABLE IF NOT EXISTS "users" (
	"Email"	TEXT NOT NULL UNIQUE,
	"Nickname"	TEXT NOT NULL,
	"Password"	TEXT NOT NULL,
	PRIMARY KEY("Email")
);
COMMIT;
