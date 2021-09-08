/*
 Navicat Premium Data Transfer

 Source Server         : Local
 Source Server Type    : PostgreSQL
 Source Server Version : 130003
 Source Host           : localhost:5432
 Source Catalog        : hogwarts
 Source Schema         : public

 Target Server Type    : PostgreSQL
 Target Server Version : 130003
 File Encoding         : 65001

 Date: 08/09/2021 18:47:46
*/


-- ----------------------------
-- Table structure for person
-- ----------------------------
CREATE TABLE "public"."person" (
  "person_id" varchar(10) COLLATE "pg_catalog"."default" NOT NULL,
  "first_name" varchar(20) COLLATE "pg_catalog"."default" NOT NULL,
  "last_name" varchar(20) COLLATE "pg_catalog"."default" NOT NULL,
  "age" int2 NOT NULL
)
;

-- ----------------------------
-- Table structure for houses
-- ----------------------------
CREATE TABLE "public"."houses" (
  "name" varchar(30) COLLATE "pg_catalog"."default" NOT NULL
)
;

-- ----------------------------
-- Table structure for applications
-- ----------------------------
CREATE TABLE "public"."applications" (
  "application_id" int2 NOT NULL DEFAULT nextval('applications_application_id_seq'::regclass),
  "person_id" varchar(10) COLLATE "pg_catalog"."default" NOT NULL,
  "aspired_house" varchar(30) COLLATE "pg_catalog"."default" NOT NULL
)
;

-- ----------------------------
-- Primary Key structure for table person
-- ----------------------------
ALTER TABLE "public"."person" ADD CONSTRAINT "person_pkey" PRIMARY KEY ("person_id");

-- ----------------------------
-- Primary Key structure for table houses
-- ----------------------------
ALTER TABLE "public"."houses" ADD CONSTRAINT "houses_pkey" PRIMARY KEY ("name");

-- ----------------------------
-- Uniques structure for table applications
-- ----------------------------
ALTER TABLE "public"."applications" ADD CONSTRAINT "applications_person_id_aspired_house_key" UNIQUE ("person_id", "aspired_house");

-- ----------------------------
-- Primary Key structure for table applications
-- ----------------------------
ALTER TABLE "public"."applications" ADD CONSTRAINT "applications_pkey" PRIMARY KEY ("application_id");

-- ----------------------------
-- Foreign Keys structure for table applications
-- ----------------------------
ALTER TABLE "public"."applications" ADD CONSTRAINT "applications_aspired_house_fkey" FOREIGN KEY ("aspired_house") REFERENCES "public"."houses" ("name") ON DELETE RESTRICT ON UPDATE CASCADE;
ALTER TABLE "public"."applications" ADD CONSTRAINT "applications_person_id_fkey" FOREIGN KEY ("person_id") REFERENCES "public"."person" ("person_id") ON DELETE RESTRICT ON UPDATE CASCADE;