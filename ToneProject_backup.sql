--
-- PostgreSQL database dump
--

-- Dumped from database version 17.0
-- Dumped by pg_dump version 17.0

-- Started on 2024-11-11 08:58:53

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
-- TOC entry 4800 (class 1262 OID 16392)
-- Name: ToneProject; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE "ToneProject" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Korean_Korea.949';


ALTER DATABASE "ToneProject" OWNER TO postgres;

\connect "ToneProject"

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
-- TOC entry 218 (class 1259 OID 16535)
-- Name: SnakeGameRecords; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."SnakeGameRecords" (
    "UserId" character varying(30) NOT NULL,
    "PlayedDate" date DEFAULT CURRENT_DATE NOT NULL,
    "GameClear" boolean NOT NULL,
    "PlayTime" integer NOT NULL,
    "Score" integer NOT NULL
);


ALTER TABLE public."SnakeGameRecords" OWNER TO postgres;

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
-- TOC entry 4794 (class 0 OID 16535)
-- Dependencies: 218
-- Data for Name: SnakeGameRecords; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."SnakeGameRecords" VALUES ('user', '2024-11-06', true, 600, 21);
INSERT INTO public."SnakeGameRecords" VALUES ('user', '2024-11-06', true, 600, 30);
INSERT INTO public."SnakeGameRecords" VALUES ('user', '2024-11-06', true, 600, 28);
INSERT INTO public."SnakeGameRecords" VALUES ('user', '2024-11-06', false, 150, 5);
INSERT INTO public."SnakeGameRecords" VALUES ('user', '2024-11-06', false, 550, 30);
INSERT INTO public."SnakeGameRecords" VALUES ('user', '2024-11-06', true, 600, 27);
INSERT INTO public."SnakeGameRecords" VALUES ('admin', '2024-11-01', true, 600, 20);
INSERT INTO public."SnakeGameRecords" VALUES ('admin', '2024-11-02', false, 500, 17);
INSERT INTO public."SnakeGameRecords" VALUES ('admin', '2024-11-03', true, 600, 22);
INSERT INTO public."SnakeGameRecords" VALUES ('admin', '2024-11-04', false, 300, 10);
INSERT INTO public."SnakeGameRecords" VALUES ('admin', '2024-11-05', true, 600, 22);
INSERT INTO public."SnakeGameRecords" VALUES ('admin', '2024-11-06', false, 5, 1);


--
-- TOC entry 4793 (class 0 OID 16419)
-- Dependencies: 217
-- Data for Name: UserInfo; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."UserInfo" VALUES ('test', 'tttest123!', 'test', '2000-11-22', '선택안함');
INSERT INTO public."UserInfo" VALUES ('user', 'userqq1!', 'user', '2000-11-11', '남');
INSERT INTO public."UserInfo" VALUES ('user1', 'uuuser1!', 'user1', '1998-05-28', '남');
INSERT INTO public."UserInfo" VALUES ('admin', 'admin', 'admin', '1999-10-10', '선택안함');


--
-- TOC entry 4646 (class 2606 OID 16511)
-- Name: UserInfo PK_UserInfo; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."UserInfo"
    ADD CONSTRAINT "PK_UserInfo" PRIMARY KEY ("Id");


--
-- TOC entry 4647 (class 2606 OID 16539)
-- Name: SnakeGameRecords fk_userId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."SnakeGameRecords"
    ADD CONSTRAINT "fk_userId" FOREIGN KEY ("UserId") REFERENCES public."UserInfo"("Id") ON DELETE CASCADE;


-- Completed on 2024-11-11 08:58:54

--
-- PostgreSQL database dump complete
--

