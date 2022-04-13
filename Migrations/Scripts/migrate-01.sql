﻿CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Weather" (
    "Id" integer GENERATED BY DEFAULT AS IDENTITY,
    "Date" timestamp without time zone NOT NULL,
    "Time" timestamp without time zone NOT NULL,
    "AirTemperature" real NOT NULL,
    "AirHumidity" integer NOT NULL,
    "DewPointTemperature" real NOT NULL,
    "AtmosphericPressure" integer NOT NULL,
    "WindDirection" text NULL,
    "WindSpeed" integer NULL,
    "Cloudiness" integer NULL,
    "LowerCloudinessLimit" real NULL,
    "HorizontalVisibility" text NULL,
    "WeatherEvent" text NULL,
    CONSTRAINT "PK_Weather" PRIMARY KEY ("Id")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20220413204320_InitWeatherDb', '5.0.15');

COMMIT;