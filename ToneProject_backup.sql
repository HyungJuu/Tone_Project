--
-- PostgreSQL database dump
--

-- Dumped from database version 17.0
-- Dumped by pg_dump version 17.0

-- Started on 2024-11-19 17:52:17

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
-- TOC entry 4805 (class 1262 OID 16392)
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
-- Name: SnakeGameHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."SnakeGameHistory" (
    "UserId" character varying(30) NOT NULL,
    "PlayedDate" date DEFAULT CURRENT_DATE NOT NULL,
    "GameClear" boolean NOT NULL,
    "PlayTime" integer NOT NULL,
    "Score" integer NOT NULL,
    "Id" integer NOT NULL
);


ALTER TABLE public."SnakeGameHistory" OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 24580)
-- Name: SnakeGameHistory_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."SnakeGameHistory_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public."SnakeGameHistory_Id_seq" OWNER TO postgres;

--
-- TOC entry 4806 (class 0 OID 0)
-- Dependencies: 219
-- Name: SnakeGameHistory_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."SnakeGameHistory_Id_seq" OWNED BY public."SnakeGameHistory"."Id";


--
-- TOC entry 217 (class 1259 OID 16419)
-- Name: UserInfo; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."UserInfo" (
    "UserId" character varying(30) NOT NULL,
    "Pwd" character varying(30) NOT NULL,
    "Name" character varying(30) NOT NULL,
    "Birth" date NOT NULL,
    "Gender" character varying(10) NOT NULL
);


ALTER TABLE public."UserInfo" OWNER TO postgres;

--
-- TOC entry 4646 (class 2604 OID 24587)
-- Name: SnakeGameHistory Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."SnakeGameHistory" ALTER COLUMN "Id" SET DEFAULT nextval('public."SnakeGameHistory_Id_seq"'::regclass);


--
-- TOC entry 4798 (class 0 OID 16535)
-- Dependencies: 218
-- Data for Name: SnakeGameHistory; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."SnakeGameHistory" VALUES ('user', '2024-11-19', false, 6, 2, 1);


--
-- TOC entry 4797 (class 0 OID 16419)
-- Dependencies: 217
-- Data for Name: UserInfo; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."UserInfo" VALUES ('test', 'tttest123!', 'test', '2000-11-22', '선택안함');
INSERT INTO public."UserInfo" VALUES ('user', 'userqq1!', 'user', '2000-11-11', '남');


--
-- TOC entry 4807 (class 0 OID 0)
-- Dependencies: 219
-- Name: SnakeGameHistory_Id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."SnakeGameHistory_Id_seq"', 1, true);


--
-- TOC entry 4648 (class 2606 OID 16511)
-- Name: UserInfo PK_UserInfo; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."UserInfo"
    ADD CONSTRAINT "PK_UserInfo" PRIMARY KEY ("UserId");


--
-- TOC entry 4650 (class 2606 OID 24586)
-- Name: SnakeGameHistory SnakeGameHistories_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."SnakeGameHistory"
    ADD CONSTRAINT "SnakeGameHistories_pkey" PRIMARY KEY ("Id");


--
-- TOC entry 4651 (class 2606 OID 16539)
-- Name: SnakeGameHistory fk_userId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."SnakeGameHistory"
    ADD CONSTRAINT "fk_userId" FOREIGN KEY ("UserId") REFERENCES public."UserInfo"("UserId") ON DELETE CASCADE;


-- Completed on 2024-11-19 17:52:17

--
-- PostgreSQL database dump complete
--

