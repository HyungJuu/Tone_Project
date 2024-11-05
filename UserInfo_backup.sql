--
-- PostgreSQL database dump
--

-- Dumped from database version 17.0
-- Dumped by pg_dump version 17.0

-- Started on 2024-11-05 17:41:50

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 4793 (class 1262 OID 16392)
-- Name: ToneProject; Type: DATABASE; Schema: -; Owner: postgres
--

ALTER DATABASE "ToneProject" OWNER TO postgres;

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 217 (class 1259 OID 16419)
-- Name: UserInfo; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."UserInfo" (
    "Id" character varying(30) NOT NULL,
    "Pwd" character varying(30) NOT NULL,
    "Name" character varying(30) NOT NULL,
    "Birth" date NOT NULL,
    "Gender" character varying(10) NOT NULL
);


ALTER TABLE public."UserInfo" OWNER TO postgres;

--
-- TOC entry 4787 (class 0 OID 16419)
-- Dependencies: 217
-- Data for Name: UserInfo; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."UserInfo" ("Id", "Pwd", "Name", "Birth", "Gender") VALUES 
    ('test', 'tttest123!', 'test', '2000-11-22', '선택안함'),
    ('rmsdk', 'rlarmsdk0308!', 'T', '1999-04-08', '여');


--
-- TOC entry 4641 (class 2606 OID 16511)
-- Name: UserInfo PK_UserInfo; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."UserInfo"
    ADD CONSTRAINT "PK_UserInfo" PRIMARY KEY ("Id");


-- Completed on 2024-11-05 17:41:50

--
-- PostgreSQL database dump complete
--

