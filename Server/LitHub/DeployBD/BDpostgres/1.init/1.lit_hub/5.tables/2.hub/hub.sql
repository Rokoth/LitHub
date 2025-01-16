drop table if not exists lit_hub.hub;

create table lit_hub.hub(
    id     uuid    not null default uuid_generate_v4()
  , name   varchar not null
  , "path" varchar not null
  , author_id uuid not null
  , owner_id   uuid not null
  , is_deleted bool not null default false
  , version_date timestamptz not null default now()
  , create_date timestamptz not null default now()
);