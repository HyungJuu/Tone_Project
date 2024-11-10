PGDMP      $            
    |            ToneProject    17.0    17.0 
    �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                           false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                           false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                           false            �           1262    16392    ToneProject    DATABASE     ~   CREATE DATABASE "ToneProject" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Korean_Korea.949';
    DROP DATABASE "ToneProject";
                     postgres    false            �            1259    16535    SnakeGameRecords    TABLE     �   CREATE TABLE public."SnakeGameRecords" (
    "UserId" character varying(30) NOT NULL,
    "PlayedDate" date DEFAULT CURRENT_DATE NOT NULL,
    "GameClear" boolean NOT NULL,
    "PlayTime" integer NOT NULL,
    "Score" integer NOT NULL
);
 &   DROP TABLE public."SnakeGameRecords";
       public         heap r       postgres    false            �            1259    16419    UserInfo    TABLE     �   CREATE TABLE public."UserInfo" (
    "Id" character varying(30) NOT NULL,
    "Pwd" character varying(30) NOT NULL,
    "Name" character varying(30) NOT NULL,
    "Birth" date NOT NULL,
    "Gender" character varying(10) NOT NULL
);
    DROP TABLE public."UserInfo";
       public         heap r       postgres    false            �          0    16535    SnakeGameRecords 
   TABLE DATA                 public               postgres    false    218   �
       �          0    16419    UserInfo 
   TABLE DATA                 public               postgres    false    217   "       &           2606    16511    UserInfo PK_UserInfo 
   CONSTRAINT     X   ALTER TABLE ONLY public."UserInfo"
    ADD CONSTRAINT "PK_UserInfo" PRIMARY KEY ("Id");
 B   ALTER TABLE ONLY public."UserInfo" DROP CONSTRAINT "PK_UserInfo";
       public                 postgres    false    217            '           2606    16539    SnakeGameRecords fk_userId    FK CONSTRAINT     �   ALTER TABLE ONLY public."SnakeGameRecords"
    ADD CONSTRAINT "fk_userId" FOREIGN KEY ("UserId") REFERENCES public."UserInfo"("Id") ON DELETE CASCADE;
 H   ALTER TABLE ONLY public."SnakeGameRecords" DROP CONSTRAINT "fk_userId";
       public               postgres    false    217    218    4646            �      x���v
Q���W((M��L�S
�K�NuO�MJM�/J)VRs�	uV�P/-N-R�QP7202�54�50�J�JSu�t�5��<�k����4����i�9�@CM���R�LSSZy�h& 9�z      �   �   x���v
Q���W((M��L�S
-N-��K�WRs�	uV�P/I-.Q�QP/)����<��������������e���o�v���B]Ӛ˓8+J�B � ���P�F����{�D��`m��`&�`�������������\\ �R�     