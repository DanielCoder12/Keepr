CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8 COMMENT '';

ALTER TABLE accounts ADD coverImg VARCHAR(500)

CREATE TABLE
    IF NOT EXISTS keeps(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        creatorId VARCHAR(255) NOT NULL,
        name VARCHAR(30) NOT NULL,
        description VARCHAR(500) NOT NULL,
        img VARCHAR(500) NOT NULL,
        views INT NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

DROP TABLE keeps;

CREATE TABLE
    IF NOT EXISTS vaults(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        creatorId VARCHAR(255) NOT NULL,
        name VARCHAR(50) NOT NULL,
        description VARCHAR(500) NOT NULL,
        img VARCHAR(500) NOT NULL,
        isPrivate BOOLEAN DEFAULT false,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

DROP TABLE vaults

CREATE TABLE
    IF NOT EXISTS vaultKeeps(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        creatorId VARCHAR(255) NOT NULL,
        vaultId INT NOT NULL,
        keepId INT NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE,
        FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE,
        FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE,
        UNIQUE (vaultId, keepId)
    ) default charset utf8 COMMENT '';

drop table vaultKeeps;

SELECT
    keeps.*,
    COUNT(vaultKeeps.id) AS kept,
    accounts.*
FROM keeps
    JOIN accounts ON keeps.creatorId = accounts.id
    LEFT JOIN vaultKeeps ON vaultKeeps.keepId = keeps.id
WHERE keeps.id = 1
GROUP BY(keeps.id);

UPDATE keeps SET views = views + 1 WHERE keeps.id = @keepId LIMIT 1;

SELECT
    keeps.*,
    COUNT(vaultKeeps.id) AS kept,
    accounts.*
FROM keeps
    JOIN accounts ON keeps.creatorId = accounts.id
    LEFT JOIN vaultKeeps ON vaultKeeps.keepId = keeps.id
WHERE keeps.id = 11
GROUP BY(keeps.id);

SELECT vaults.*, accounts.*
FROM vaults
    JOIN accounts ON accounts.id = vaults.`creatorId`
WHERE vaults.id = 1;

INSERT INTO
    vaultKeeps(creatorId, vaultId, keepId)
VALUES (@CreatorId, @VaultId, @Id);

SELECT
    vaultKeeps.*,
    accounts.*,
    keeps.*,
    COUNT(vk.id) AS kept
FROM vaultKeeps
    JOIN accounts ON accounts.id = vaultKeeps.creatorId
    JOIN keeps ON keeps.id = vaultKeeps.keepId
    LEFT JOIN vaultKeeps vk ON vk.keepId = keeps.id
WHERE vaultKeeps.id = 3
GROUP BY(keeps.id);

SELECT
    keeps.*,
    COUNT(vaultKeeps.id) AS kept,
    accounts.*
FROM keeps
    JOIN accounts ON keeps.creatorId = accounts.id
    LEFT JOIN vaultKeeps ON vaultKeeps.keepId = keeps.id
WHERE keeps.id = 1
GROUP BY(keeps.id);

SELECT
    vaultKeeps.*,
    accounts.*,
    keeps.*,
    COUNT(vk.id) AS kept
FROM vaultKeeps
    JOIN accounts ON accounts.id = vaultKeeps.creatorId
    JOIN keeps ON keeps.id = vaultKeeps.keepId
    LEFT JOIN vaultKeeps vk ON vk.keepId = keeps.id
WHERE vaultKeeps.vaultId = 40
GROUP BY(keeps.id);

SELECT * FROM vaultKeeps WHERE vaultId = 130;

SELECT * FROM accounts WHERE id = '6541814359b43990cb08b854';

SELECT * FROM keeps WHERE id = 94;

SELECT
    vaultKeeps.*,
    accounts.*,
    keeps.*,
    COUNT(vk.id) AS kept
FROM vaultKeeps
    JOIN accounts ON accounts.id = vaultKeeps.creatorId
    JOIN keeps ON keeps.id = vaultKeeps.keepId
    LEFT JOIN vaultKeeps vk ON vk.keepId = keeps.id
WHERE vaultKeeps.vaultId = 64
GROUP BY(keeps.id);

UPDATE accounts
SET
    name = 'minecraft',
    picture = 'https://s.gravatar.com/avatar/c679f0317f1920dc6165123424752ae1?s=480&r=pg&d=https%3A%2F%2Fcdn.auth0.com%2Favatars%2Fmi.png',
    coverImg = 'https://plus.unsplash.com/premium_photo-1701188698374-c5e24b3fbeab?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D'
WHERE
    id = '6541814359b43990cb08b854'
LIMIT 1;