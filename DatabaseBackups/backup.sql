--
-- PostgreSQL database dump
--

-- Dumped from database version 12.3
-- Dumped by pg_dump version 12.2

-- Started on 2020-07-07 13:49:11 CDT

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

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 205 (class 1259 OID 16480)
-- Name: messages; Type: TABLE; Schema: public; Owner: reid
--

CREATE TABLE public.messages (
    id integer NOT NULL,
    user_id integer NOT NULL,
    text text,
    created_date timestamp without time zone NOT NULL
);


ALTER TABLE public.messages OWNER TO reid;

--
-- TOC entry 204 (class 1259 OID 16478)
-- Name: messages_id_seq; Type: SEQUENCE; Schema: public; Owner: reid
--

CREATE SEQUENCE public.messages_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.messages_id_seq OWNER TO reid;

--
-- TOC entry 3223 (class 0 OID 0)
-- Dependencies: 204
-- Name: messages_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: reid
--

ALTER SEQUENCE public.messages_id_seq OWNED BY public.messages.id;


--
-- TOC entry 206 (class 1259 OID 16561)
-- Name: sessions_sequence; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.sessions_sequence
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.sessions_sequence OWNER TO postgres;

--
-- TOC entry 207 (class 1259 OID 16563)
-- Name: sessions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.sessions (
    id integer DEFAULT nextval('public.sessions_sequence'::regclass) NOT NULL,
    user_id integer NOT NULL,
    created_date timestamp without time zone DEFAULT now()
);


ALTER TABLE public.sessions OWNER TO postgres;

--
-- TOC entry 203 (class 1259 OID 16471)
-- Name: users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.users (
    id integer NOT NULL,
    username text,
    created_date timestamp without time zone,
    password text
);


ALTER TABLE public.users OWNER TO postgres;

--
-- TOC entry 202 (class 1259 OID 16469)
-- Name: users_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.users_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.users_id_seq OWNER TO postgres;

--
-- TOC entry 3224 (class 0 OID 0)
-- Dependencies: 202
-- Name: users_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.users_id_seq OWNED BY public.users.id;


--
-- TOC entry 3074 (class 2604 OID 16483)
-- Name: messages id; Type: DEFAULT; Schema: public; Owner: reid
--

ALTER TABLE ONLY public.messages ALTER COLUMN id SET DEFAULT nextval('public.messages_id_seq'::regclass);


--
-- TOC entry 3073 (class 2604 OID 16474)
-- Name: users id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users ALTER COLUMN id SET DEFAULT nextval('public.users_id_seq'::regclass);


--
-- TOC entry 3215 (class 0 OID 16480)
-- Dependencies: 205
-- Data for Name: messages; Type: TABLE DATA; Schema: public; Owner: reid
--

COPY public.messages (id, user_id, text, created_date) FROM stdin;
10	56	nyc	0001-01-01 00:00:00
11	56	nyc	0001-01-01 00:00:00
12	56	nyc	0001-01-01 00:00:00
13	56	nyc	0001-01-01 00:00:00
14	56	nyc	2020-07-04 11:15:47.662287
16	56	nyc	2020-07-04 11:23:05.207903
17	56	nyc	2020-07-04 11:28:57.823457
18	56	nyc	2020-07-04 11:32:37.607733
22	56	nyc	2020-07-04 11:41:13.98891
23	56	nyc	2020-07-04 11:43:20.949805
24	56	HWen is this going to work?	2020-07-07 08:31:00.000336
25	56	Hi again!	2020-07-07 08:37:19.854319
\.


--
-- TOC entry 3217 (class 0 OID 16563)
-- Dependencies: 207
-- Data for Name: sessions; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.sessions (id, user_id, created_date) FROM stdin;
1	1	2020-06-23 10:22:54.650444
2	1	2020-06-23 10:23:55.668912
3	1	2020-06-23 10:25:58.70484
4	1	2020-06-23 10:26:57.770926
5	1	2020-06-23 10:27:20.987495
6	1	2020-06-23 10:30:41.833928
7	1	2020-06-23 10:30:48.668112
8	1	2020-06-23 10:30:54.226107
9	1	2020-06-23 10:33:25.82048
10	1	2020-06-23 10:33:47.989256
11	1	2020-06-23 10:34:09.055899
12	1	2020-06-23 10:34:35.805352
13	1	2020-06-23 10:36:22.502327
14	1	2020-06-23 10:37:37.590936
15	1	2020-06-23 10:37:45.790173
16	1	2020-06-23 10:43:34.238923
17	1	2020-06-23 10:48:22.051366
18	1	2020-06-23 10:48:38.871762
19	1	2020-06-23 10:49:24.89416
20	1	2020-06-23 10:49:28.999027
21	1	2020-06-23 10:50:33.537325
22	1	2020-06-23 10:51:11.155819
23	1	2020-06-23 10:53:46.703289
24	1	2020-06-23 10:53:52.926673
25	1	2020-06-23 10:53:54.628228
26	1	2020-06-23 10:57:31.813305
27	4	2020-06-24 21:51:47.22275
28	5	2020-06-24 21:52:40.969739
29	6	2020-06-25 09:13:35.167179
30	7	2020-06-25 09:16:57.248271
31	8	2020-06-25 09:18:30.201658
32	9	2020-06-25 09:26:06.487824
33	1	2020-06-25 09:26:24.330514
34	10	2020-06-25 09:28:57.894341
35	11	2020-06-25 09:29:40.695243
36	12	2020-06-25 09:30:24.084377
37	13	2020-06-25 09:31:19.57214
38	14	2020-06-25 09:31:24.255708
39	15	2020-06-25 09:31:58.013736
40	16	2020-06-25 09:32:49.519534
41	17	2020-06-25 14:02:59.062956
42	18	2020-06-25 14:04:13.270079
43	19	2020-06-25 14:07:08.313067
44	20	2020-06-25 14:09:25.308115
45	21	2020-06-25 15:19:59.637882
46	22	2020-06-25 15:20:09.148894
47	24	2020-06-26 08:32:42.277456
48	25	2020-06-26 08:32:52.182561
49	26	2020-06-26 08:34:14.683625
50	27	2020-06-26 08:35:19.731302
51	28	2020-06-26 08:35:58.219086
52	35	2020-06-26 13:00:38.446995
53	36	2020-06-26 13:08:46.332629
54	37	2020-06-26 13:14:59.748899
55	38	2020-06-26 13:28:52.438729
56	39	2020-06-26 13:31:19.764891
57	40	2020-06-26 14:01:55.573768
58	41	2020-06-26 14:07:21.238276
59	42	2020-06-26 16:20:58.02172
68	37	2020-06-29 18:02:40.28642
69	37	2020-06-29 18:09:34.783365
71	37	2020-06-29 18:13:05.330865
83	43	2020-06-30 16:09:56.51541
87	44	2020-06-30 16:56:26.662064
88	45	2020-06-30 17:00:49.573922
92	46	2020-07-01 08:39:28.981289
93	37	2020-07-01 13:08:02.213716
94	37	2020-07-01 13:09:27.376539
95	37	2020-07-01 13:10:32.394938
96	37	2020-07-01 13:14:20.259634
97	37	2020-07-01 13:14:58.459585
98	37	2020-07-01 13:15:05.176281
99	37	2020-07-01 15:59:40.046673
100	49	2020-07-01 16:26:31.70788
101	37	2020-07-01 16:45:10.395435
102	37	2020-07-01 16:45:13.645262
103	37	2020-07-01 16:48:00.410551
104	37	2020-07-01 16:48:03.471965
105	37	2020-07-01 16:49:24.020491
106	37	2020-07-01 16:49:26.898789
107	49	2020-07-01 17:18:49.574498
108	49	2020-07-01 17:51:18.817293
109	49	2020-07-01 18:18:55.824231
110	49	2020-07-01 18:18:59.521687
111	49	2020-07-01 18:23:19.802034
112	49	2020-07-01 18:23:21.813673
113	49	2020-07-01 18:27:34.565095
114	49	2020-07-01 18:28:05.380074
115	49	2020-07-02 15:53:23.036481
116	49	2020-07-02 15:59:59.082065
117	49	2020-07-02 16:02:47.644443
118	49	2020-07-02 17:46:58.197782
119	49	2020-07-02 18:36:33.177078
120	49	2020-07-02 18:36:34.838906
121	49	2020-07-02 18:37:43.435351
122	49	2020-07-02 18:37:46.039227
123	49	2020-07-02 18:37:46.6552
124	49	2020-07-02 18:37:53.634576
125	49	2020-07-02 18:38:08.319498
126	49	2020-07-02 20:08:51.278837
127	49	2020-07-02 20:08:54.122833
128	49	2020-07-02 20:09:40.740146
129	49	2020-07-02 20:09:44.257596
130	49	2020-07-02 20:11:29.935462
131	49	2020-07-02 20:11:32.317803
132	49	2020-07-02 20:12:43.313344
133	49	2020-07-02 20:12:56.254301
134	49	2020-07-02 20:12:58.075712
135	49	2020-07-02 20:13:06.11825
136	49	2020-07-02 20:14:34.601536
137	49	2020-07-02 20:14:58.955875
138	49	2020-07-02 20:15:24.50586
139	49	2020-07-03 06:02:39.387538
140	49	2020-07-03 06:05:00.109482
141	49	2020-07-03 06:13:16.400384
142	49	2020-07-03 06:14:00.655583
143	49	2020-07-03 10:42:07.058858
144	49	2020-07-03 10:44:26.203875
145	49	2020-07-03 10:49:24.202251
146	49	2020-07-03 10:58:03.935701
147	49	2020-07-03 11:01:24.342537
148	49	2020-07-03 11:04:15.556
149	49	2020-07-03 11:07:13.768763
150	49	2020-07-03 11:07:22.961684
151	49	2020-07-03 11:08:25.954833
152	49	2020-07-03 14:45:32.554949
153	49	2020-07-03 14:46:16.501542
154	49	2020-07-03 14:52:27.378325
155	37	2020-07-03 14:52:41.804418
156	49	2020-07-03 19:49:21.765323
157	50	2020-07-03 19:50:34.891634
158	51	2020-07-03 19:56:06.01083
159	52	2020-07-03 19:56:13.719091
160	53	2020-07-03 19:57:06.637676
161	54	2020-07-03 19:57:11.512366
162	55	2020-07-03 20:00:29.528331
163	56	2020-07-03 20:01:48.945035
164	49	2020-07-04 13:36:43.430209
165	49	2020-07-06 10:23:41.36234
166	49	2020-07-06 10:23:41.362352
167	49	2020-07-06 10:24:51.532725
168	49	2020-07-06 10:24:51.548756
169	49	2020-07-06 10:26:08.530385
170	49	2020-07-06 10:27:36.328103
171	49	2020-07-06 10:39:09.503967
172	49	2020-07-06 10:41:43.182641
173	49	2020-07-06 13:56:38.679715
174	49	2020-07-06 13:59:39.547812
175	49	2020-07-06 14:09:09.687068
176	49	2020-07-06 14:12:33.161835
177	49	2020-07-06 14:13:45.292795
178	49	2020-07-06 14:18:26.221539
179	49	2020-07-06 14:21:38.479504
180	49	2020-07-06 14:22:37.021057
181	49	2020-07-06 14:24:01.059727
182	49	2020-07-06 14:26:38.176968
183	49	2020-07-06 14:27:45.774009
184	49	2020-07-06 14:27:56.780278
185	49	2020-07-06 14:28:32.997977
186	49	2020-07-06 14:29:04.25155
187	49	2020-07-06 14:34:13.086553
188	49	2020-07-06 14:34:54.013089
189	49	2020-07-06 14:39:44.900717
190	49	2020-07-06 14:47:54.951351
191	49	2020-07-06 14:48:07.461392
192	49	2020-07-06 14:48:32.930487
193	49	2020-07-06 14:49:58.638007
194	49	2020-07-06 14:52:35.557099
195	49	2020-07-06 14:53:31.128989
196	49	2020-07-06 14:53:50.065639
197	49	2020-07-06 14:55:02.407855
198	49	2020-07-06 15:20:25.157139
\.


--
-- TOC entry 3213 (class 0 OID 16471)
-- Dependencies: 203
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.users (id, username, created_date, password) FROM stdin;
1	Again.	2020-06-11 19:13:04.040432	\N
2	and Again.	2020-06-11 19:15:28.331571	\N
3	birdup	2020-06-12 10:24:12.906925	\N
4	yo	\N	yowzers
5	yo	\N	yowzers
6	reidmuchow	\N	yes
7	no	0001-01-01 00:00:00	yes
8	yes	2020-06-25 09:18:30.190533	no
9	yesandno	2020-06-25 09:26:06.485486	noandyes
10	youbetcha	2020-06-25 09:28:57.881088	1985
11	youbetcha	2020-06-25 09:29:40.692235	1985
12	youbetcha	2020-06-25 09:30:24.072893	1985
13	youbetcha	2020-06-25 09:31:19.560483	1985
14		2020-06-25 09:31:24.25294	
15		2020-06-25 09:31:57.999385	
16		2020-06-25 09:32:49.5079	
17		2020-06-25 14:02:59.041907	
18		2020-06-25 14:04:13.267081	
19		2020-06-25 14:07:08.299075	
20		2020-06-25 14:09:25.303997	
21	yjutyutyu	2020-06-25 15:19:59.626554	tyutyu
22	dfgfdgh	2020-06-25 15:20:09.14541	dfghegh
23	reidmuchow	2020-06-26 08:22:59.733143	gobs
24	reidmuchow	2020-06-26 08:32:42.265952	gobs
25	reidmuchow	2020-06-26 08:32:52.180315	gobs
26	reidmuchow	2020-06-26 08:34:14.671836	edfgerg
27	reidmuchow	2020-06-26 08:35:19.719289	sdgsrgf
28	reidmuchow	2020-06-26 08:35:58.216536	ertwrt
29	reidmuchow	2020-06-26 08:38:20.55314	ertwrt
30	reidmuchow	2020-06-26 08:39:57.985091	uwdhfioew
31	reidmuchow	2020-06-26 08:42:38.095724	uwdhfioew
32	reidmuchow	2020-06-26 08:44:14.871991	uwdhfioew
33	refgerg	2020-06-26 10:01:10.876081	gfg
34	reidmuchow	2020-06-26 12:53:28.810182	derer
35	reidmuchow	2020-06-26 13:00:38.440913	derer
36	reidisdaman	2020-06-26 13:08:46.325566	derer
37	roast	2020-06-26 13:14:59.742262	asdfadsf
38	reidmuchow	2020-06-26 13:28:52.432191	fesdfsdf
39	reidmuchow	2020-06-26 13:31:19.758687	dfgerg
40	reidmuchow	2020-06-26 14:01:55.567907	rger
41	reidmuchow	2020-06-26 14:07:21.232373	rger
42	reidmuchow	2020-06-26 16:20:57.992161	rger
43	bees	2020-06-30 16:09:56.507999	pebs
44	rtoity	2020-06-30 16:56:26.65614	pebs
45	ereijri	2020-06-30 17:00:42.764623	pebs
46	red	2020-07-01 08:39:28.96499	asd
47	natty	2020-07-01 11:41:29.445545	asd
48	nattys	2020-07-01 11:57:43.077446	asd
49	admin	2020-07-01 16:26:27.800609	reid
50	madlib	2020-07-03 19:50:34.881379	shadesofblue
51	lilwayne	2020-07-03 19:56:06.009692	neworleans
52	lilwayne	2020-07-03 19:56:13.717794	neworleans
53	lilwayne	2020-07-03 19:57:06.636413	neworleans
54	lilwayne	2020-07-03 19:57:11.511052	neworleans
55	peterock	2020-07-03 20:00:29.525271	nyc
56	mosdef	2020-07-03 20:01:48.942645	blackonbothsides
\.


--
-- TOC entry 3225 (class 0 OID 0)
-- Dependencies: 204
-- Name: messages_id_seq; Type: SEQUENCE SET; Schema: public; Owner: reid
--

SELECT pg_catalog.setval('public.messages_id_seq', 25, true);


--
-- TOC entry 3226 (class 0 OID 0)
-- Dependencies: 206
-- Name: sessions_sequence; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.sessions_sequence', 198, true);


--
-- TOC entry 3227 (class 0 OID 0)
-- Dependencies: 202
-- Name: users_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.users_id_seq', 56, true);


--
-- TOC entry 3081 (class 2606 OID 16490)
-- Name: messages messages_pkey; Type: CONSTRAINT; Schema: public; Owner: reid
--

ALTER TABLE ONLY public.messages
    ADD CONSTRAINT messages_pkey PRIMARY KEY (id);


--
-- TOC entry 3083 (class 2606 OID 16569)
-- Name: sessions sessions_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sessions
    ADD CONSTRAINT sessions_pkey PRIMARY KEY (id);


--
-- TOC entry 3078 (class 2606 OID 16488)
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);


--
-- TOC entry 3079 (class 1259 OID 16496)
-- Name: fki_messsages_users_fk; Type: INDEX; Schema: public; Owner: reid
--

CREATE INDEX fki_messsages_users_fk ON public.messages USING btree (user_id);


--
-- TOC entry 3084 (class 2606 OID 16497)
-- Name: messages messages_user_fk; Type: FK CONSTRAINT; Schema: public; Owner: reid
--

ALTER TABLE ONLY public.messages
    ADD CONSTRAINT messages_user_fk FOREIGN KEY (user_id) REFERENCES public.users(id);


--
-- TOC entry 3085 (class 2606 OID 16570)
-- Name: sessions sessions_user_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.sessions
    ADD CONSTRAINT sessions_user_id_fkey FOREIGN KEY (user_id) REFERENCES public.users(id);


-- Completed on 2020-07-07 13:49:11 CDT

--
-- PostgreSQL database dump complete
--

