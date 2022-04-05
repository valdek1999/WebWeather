
CREATE SCHEMA client;

CREATE TABLE client."Client"
(
    "Id" integer PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
    "ClientMobileId" integer,
    "ClientGuidInDataStorage" uuid,
    "FirstName" character varying(64),
    "LastName" character varying(64),
    "MiddleName" character varying(64),
    "Birthday" date NOT NULL,
    "DocumentSerial" character varying(16),
    "DocumentNumber" character varying(32),
    "PhoneNumber" character varying(32),
    "ClientHashGuidInDataStorage" uuid,
    "AgreementPersonalDatatime" timestamp with time zone,
    "AgreementMailing" timestamp with time zone,
    "ConfirmedEsia" boolean,
    "ClientMobileUgskId" integer,
    "ClientMobileMkbId" integer
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE client."Contract"
(
    "Id" integer NOT NULL,
    "ClientHashGuidInDataStorage" uuid,
    "ContractGuidInDataStorage" uuid,
    "ParentContractGuidInDataStorage" uuid,
    "SourceSystemName" text,
    "ProcessingDate" timestamp with time zone
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE client."DraftContract"
(
    "Id" integer NOT NULL,
    "ParentGuidContract" uuid NOT NULL,
    "DraftGuidContract" uuid NOT NULL,
    "InvoicePayment" text,
    "DataContract" text,
    "ProcessingDate" timestamp with time zone
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

INSERT INTO client."DraftContract" ("Id", "ParentGuidContract","DraftGuidContract","ProcessingDate") VALUES (1, '08df5f56-2978-4a24-b2c6-0f1835cbc7a3', '29ac64f2-5623-4d07-b4a0-b820b10f3999', '2021-10-28T12:17:57Z');

CREATE SCHEMA kias;

CREATE TABLE kias.object
(
    recordid bigint,
    datetimestamp text,
    "timestamp" text,
    schemalink text,
    objectlink text,
    sourcesystemobjectid uuid,
    data jsonb
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE SCHEMA upro;

CREATE TABLE upro.object
(
    recordid bigint,
    datetimestamp text,
    "timestamp" text,
    schemalink text,
    objectlink text,
    sourcesystemobjectid uuid,
    data jsonb
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE SCHEMA csia;

CREATE TABLE csia.object
(
    recordid bigint,
    datetimestamp text,
    "timestamp" text,
    schemalink text,
    objectlink text,
    sourcesystemobjectid uuid,
    data jsonb
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;

CREATE TABLE public.policy
(
    policy_id bigint,
    name character,
    status character,
    series character,
    "number" character,
    contract_series character,
    contract_number character,
    contract_start_date timestamp with time zone,
    contract_end_date timestamp with time zone,
    start_date timestamp with time zone,
    end_date timestamp with time zone,
    information_update_data timestamp with time zone,
    type character,
    balance numeric,
    is_prolongable boolean,
    url character,
    customer_id bigint,
    cover_alias_id bigint,
    insurance_delay integer,
    sk_id character,
    policy_version bigint,
    version bigint,
    prolongate_status character,
    pdf_available boolean NOT NULL DEFAULT false
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;