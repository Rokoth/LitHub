--
-- ���� ������������ � ������� SQLiteStudio v3.1.1 � �� ��� 19 20:03:26 2019
--
-- �������������� ��������� ������: System
--
PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- �������: hub
DROP TABLE IF EXISTS hub;
CREATE TABLE hub (hub_id INTEGER PRIMARY KEY AUTOINCREMENT, guid VARCHAR, path VARCHAR, server_id INTEGER REFERENCES server (server_id) ON DELETE CASCADE ON UPDATE NO ACTION MATCH SIMPLE, local_path VARCHAR);

-- �������: server
DROP TABLE IF EXISTS server;
CREATE TABLE server (server_id INTEGER PRIMARY KEY AUTOINCREMENT, uri VARCHAR, port INTEGER, name VARCHAR, login VARCHAR, password VARCHAR);

COMMIT TRANSACTION;
PRAGMA foreign_keys = on;
