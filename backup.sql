--
-- PostgreSQL database dump
--

-- Dumped from database version 11.11 (Ubuntu 11.11-1.pgdg18.04+1)
-- Dumped by pg_dump version 12.2

-- Started on 2021-07-15 18:47:40 CDT

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 5496 (class 1262 OID 668731)
-- Name: zabhlhvd; Type: DATABASE; Schema: -; Owner: zabhlhvd
--

CREATE DATABASE zabhlhvd WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'en_US.UTF-8' LC_CTYPE = 'en_US.UTF-8';


ALTER DATABASE zabhlhvd OWNER TO zabhlhvd;

\connect zabhlhvd

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 15 (class 3079 OID 17135)
-- Name: btree_gin; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS btree_gin WITH SCHEMA public;


--
-- TOC entry 5498 (class 0 OID 0)
-- Dependencies: 15
-- Name: EXTENSION btree_gin; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION btree_gin IS 'support for indexing common datatypes in GIN';


--
-- TOC entry 19 (class 3079 OID 17676)
-- Name: btree_gist; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS btree_gist WITH SCHEMA public;


--
-- TOC entry 5499 (class 0 OID 0)
-- Dependencies: 19
-- Name: EXTENSION btree_gist; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION btree_gist IS 'support for indexing common datatypes in GiST';


--
-- TOC entry 8 (class 3079 OID 16661)
-- Name: citext; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS citext WITH SCHEMA public;


--
-- TOC entry 5500 (class 0 OID 0)
-- Dependencies: 8
-- Name: EXTENSION citext; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION citext IS 'data type for case-insensitive character strings';


--
-- TOC entry 17 (class 3079 OID 17573)
-- Name: cube; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS cube WITH SCHEMA public;


--
-- TOC entry 5501 (class 0 OID 0)
-- Dependencies: 17
-- Name: EXTENSION cube; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION cube IS 'data type for multidimensional cubes';


--
-- TOC entry 2 (class 3079 OID 16384)
-- Name: dblink; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS dblink WITH SCHEMA public;


--
-- TOC entry 5502 (class 0 OID 0)
-- Dependencies: 2
-- Name: EXTENSION dblink; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION dblink IS 'connect to other PostgreSQL databases from within a database';


--
-- TOC entry 14 (class 3079 OID 17130)
-- Name: dict_int; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS dict_int WITH SCHEMA public;


--
-- TOC entry 5503 (class 0 OID 0)
-- Dependencies: 14
-- Name: EXTENSION dict_int; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION dict_int IS 'text search dictionary template for integers';


--
-- TOC entry 20 (class 3079 OID 18299)
-- Name: dict_xsyn; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS dict_xsyn WITH SCHEMA public;


--
-- TOC entry 5504 (class 0 OID 0)
-- Dependencies: 20
-- Name: EXTENSION dict_xsyn; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION dict_xsyn IS 'text search dictionary template for extended synonym processing';


--
-- TOC entry 18 (class 3079 OID 17660)
-- Name: earthdistance; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS earthdistance WITH SCHEMA public;


--
-- TOC entry 5505 (class 0 OID 0)
-- Dependencies: 18
-- Name: EXTENSION earthdistance; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION earthdistance IS 'calculate great-circle distances on the surface of the Earth';


--
-- TOC entry 7 (class 3079 OID 16650)
-- Name: fuzzystrmatch; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS fuzzystrmatch WITH SCHEMA public;


--
-- TOC entry 5506 (class 0 OID 0)
-- Dependencies: 7
-- Name: EXTENSION fuzzystrmatch; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION fuzzystrmatch IS 'determine similarities and distance between strings';


--
-- TOC entry 13 (class 3079 OID 17007)
-- Name: hstore; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS hstore WITH SCHEMA public;


--
-- TOC entry 5507 (class 0 OID 0)
-- Dependencies: 13
-- Name: EXTENSION hstore; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION hstore IS 'data type for storing sets of (key, value) pairs';


--
-- TOC entry 12 (class 3079 OID 16889)
-- Name: intarray; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS intarray WITH SCHEMA public;


--
-- TOC entry 5508 (class 0 OID 0)
-- Dependencies: 12
-- Name: EXTENSION intarray; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION intarray IS 'functions, operators, and index support for 1-D arrays of integers';


--
-- TOC entry 4 (class 3079 OID 16444)
-- Name: ltree; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS ltree WITH SCHEMA public;


--
-- TOC entry 5509 (class 0 OID 0)
-- Dependencies: 4
-- Name: EXTENSION ltree; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION ltree IS 'data type for hierarchical tree-like structures';


--
-- TOC entry 22 (class 3079 OID 18311)
-- Name: pg_stat_statements; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS pg_stat_statements WITH SCHEMA public;


--
-- TOC entry 5510 (class 0 OID 0)
-- Dependencies: 22
-- Name: EXTENSION pg_stat_statements; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION pg_stat_statements IS 'track execution statistics of all SQL statements executed';


--
-- TOC entry 11 (class 3079 OID 16812)
-- Name: pg_trgm; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS pg_trgm WITH SCHEMA public;


--
-- TOC entry 5511 (class 0 OID 0)
-- Dependencies: 11
-- Name: EXTENSION pg_trgm; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION pg_trgm IS 'text similarity measurement and index searching based on trigrams';


--
-- TOC entry 10 (class 3079 OID 16775)
-- Name: pgcrypto; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS pgcrypto WITH SCHEMA public;


--
-- TOC entry 5512 (class 0 OID 0)
-- Dependencies: 10
-- Name: EXTENSION pgcrypto; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION pgcrypto IS 'cryptographic functions';


--
-- TOC entry 16 (class 3079 OID 17571)
-- Name: pgrowlocks; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS pgrowlocks WITH SCHEMA public;


--
-- TOC entry 5513 (class 0 OID 0)
-- Dependencies: 16
-- Name: EXTENSION pgrowlocks; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION pgrowlocks IS 'show row-level locking information';


--
-- TOC entry 5 (class 3079 OID 16619)
-- Name: pgstattuple; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS pgstattuple WITH SCHEMA public;


--
-- TOC entry 5514 (class 0 OID 0)
-- Dependencies: 5
-- Name: EXTENSION pgstattuple; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION pgstattuple IS 'show tuple-level statistics';


--
-- TOC entry 23 (class 3079 OID 18318)
-- Name: postgis; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS postgis WITH SCHEMA public;


--
-- TOC entry 5515 (class 0 OID 0)
-- Dependencies: 23
-- Name: EXTENSION postgis; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION postgis IS 'PostGIS geometry, geography, and raster spatial types and functions';


--
-- TOC entry 6 (class 3079 OID 16629)
-- Name: tablefunc; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS tablefunc WITH SCHEMA public;


--
-- TOC entry 5516 (class 0 OID 0)
-- Dependencies: 6
-- Name: EXTENSION tablefunc; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION tablefunc IS 'functions that manipulate whole tables, including crosstab';


--
-- TOC entry 21 (class 3079 OID 18304)
-- Name: unaccent; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS unaccent WITH SCHEMA public;


--
-- TOC entry 5517 (class 0 OID 0)
-- Dependencies: 21
-- Name: EXTENSION unaccent; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION unaccent IS 'text search dictionary that removes accents';


--
-- TOC entry 9 (class 3079 OID 16764)
-- Name: uuid-ossp; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS "uuid-ossp" WITH SCHEMA public;


--
-- TOC entry 5518 (class 0 OID 0)
-- Dependencies: 9
-- Name: EXTENSION "uuid-ossp"; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION "uuid-ossp" IS 'generate universally unique identifiers (UUIDs)';


--
-- TOC entry 3 (class 3079 OID 16430)
-- Name: xml2; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS xml2 WITH SCHEMA public;


--
-- TOC entry 5519 (class 0 OID 0)
-- Dependencies: 3
-- Name: EXTENSION xml2; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION xml2 IS 'XPath querying and XSLT';


--
-- TOC entry 242 (class 1259 OID 673419)
-- Name: messages_id_seq; Type: SEQUENCE; Schema: public; Owner: zabhlhvd
--

CREATE SEQUENCE public.messages_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.messages_id_seq OWNER TO zabhlhvd;

SET default_tablespace = '';

--
-- TOC entry 239 (class 1259 OID 673283)
-- Name: messages; Type: TABLE; Schema: public; Owner: zabhlhvd
--

CREATE TABLE public.messages (
    id integer DEFAULT nextval('public.messages_id_seq'::regclass) NOT NULL,
    user_id integer NOT NULL,
    text text,
    created_date timestamp without time zone NOT NULL
);


ALTER TABLE public.messages OWNER TO zabhlhvd;

--
-- TOC entry 244 (class 1259 OID 673425)
-- Name: sessions_id_seq; Type: SEQUENCE; Schema: public; Owner: zabhlhvd
--

CREATE SEQUENCE public.sessions_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.sessions_id_seq OWNER TO zabhlhvd;

--
-- TOC entry 240 (class 1259 OID 673296)
-- Name: sessions; Type: TABLE; Schema: public; Owner: zabhlhvd
--

CREATE TABLE public.sessions (
    id integer DEFAULT nextval('public.sessions_id_seq'::regclass) NOT NULL,
    user_id integer NOT NULL,
    created_date timestamp without time zone DEFAULT now()
);


ALTER TABLE public.sessions OWNER TO zabhlhvd;

--
-- TOC entry 241 (class 1259 OID 673416)
-- Name: sessions_sequence; Type: SEQUENCE; Schema: public; Owner: zabhlhvd
--

CREATE SEQUENCE public.sessions_sequence
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.sessions_sequence OWNER TO zabhlhvd;

--
-- TOC entry 243 (class 1259 OID 673422)
-- Name: users_id_seq; Type: SEQUENCE; Schema: public; Owner: zabhlhvd
--

CREATE SEQUENCE public.users_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.users_id_seq OWNER TO zabhlhvd;

--
-- TOC entry 238 (class 1259 OID 673273)
-- Name: users; Type: TABLE; Schema: public; Owner: zabhlhvd
--

CREATE TABLE public.users (
    id integer DEFAULT nextval('public.users_id_seq'::regclass) NOT NULL,
    username text,
    created_date timestamp without time zone,
    password text,
    last_active_at timestamp without time zone
);


ALTER TABLE public.users OWNER TO zabhlhvd;

--
-- TOC entry 5357 (class 2606 OID 673290)
-- Name: messages messages_pkey; Type: CONSTRAINT; Schema: public; Owner: zabhlhvd
--

ALTER TABLE ONLY public.messages
    ADD CONSTRAINT messages_pkey PRIMARY KEY (id);


--
-- TOC entry 5359 (class 2606 OID 673301)
-- Name: sessions sessions_pkey; Type: CONSTRAINT; Schema: public; Owner: zabhlhvd
--

ALTER TABLE ONLY public.sessions
    ADD CONSTRAINT sessions_pkey PRIMARY KEY (id);


--
-- TOC entry 5355 (class 2606 OID 673280)
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: zabhlhvd
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);


--
-- TOC entry 5360 (class 2606 OID 673291)
-- Name: messages messages_user_fk; Type: FK CONSTRAINT; Schema: public; Owner: zabhlhvd
--

ALTER TABLE ONLY public.messages
    ADD CONSTRAINT messages_user_fk FOREIGN KEY (user_id) REFERENCES public.users(id);


--
-- TOC entry 5361 (class 2606 OID 673302)
-- Name: sessions sessions_user_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: zabhlhvd
--

ALTER TABLE ONLY public.sessions
    ADD CONSTRAINT sessions_user_id_fkey FOREIGN KEY (user_id) REFERENCES public.users(id);


--
-- TOC entry 5497 (class 0 OID 0)
-- Dependencies: 5496
-- Name: DATABASE zabhlhvd; Type: ACL; Schema: -; Owner: zabhlhvd
--

REVOKE CONNECT,TEMPORARY ON DATABASE zabhlhvd FROM PUBLIC;


-- Completed on 2021-07-15 18:54:17 CDT

--
-- PostgreSQL database dump complete
--

