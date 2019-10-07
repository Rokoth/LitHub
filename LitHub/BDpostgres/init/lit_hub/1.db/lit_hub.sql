drop database if exists lit_hub;

create database lit_hub
  with owner = postgres
    encoding = 'UTF8'
    tablespace = pg_default
    lc_collate = 'Russian_Russia.1251'
    lc_ctype = 'Russian_Russia.1251'
    connection limit = -1;