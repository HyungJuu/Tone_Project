# Tone_Project

- PostgreSQL에 TonePorject의 데이터베이스 받아오는 방법  
    &rarr; **✅현재 백업파일은 방법(2)에 맞춰 수정되어 있음✅**
    
    - 방법(1)
        1. 백업파일 생성시 **[Include CREATE DATABASE statement 옵션]** 을 선택했을 경우, CREATE DATABASE 쿼리 우선 실행

            ``` SQL
            CREATE DATABASE "ToneProject" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'Korean_Korea.949';
            ```
        2. 생성된 데이터베이스의 쿼리도구에서 나머지 쿼리문 실행

    - 방법(2)
        1. 동일명의 데이터베이스 생성 &rarr; ToneProject
            
            ![image](https://github.com/user-attachments/assets/de76ec30-fabc-4cb7-b4bc-8fd3b199ca40)
            
        2. 생성한 데이터베이스의 쿼리도구 실행
            
            ![image](https://github.com/user-attachments/assets/91a53549-df72-49d4-af9c-5a3a01340adc)

        3. 데이터베이스 백업파일 UserInfo_backup.sql 붙여넣기
            - 데이터 없이 테이블 속성만 받고 싶다면 sql 파일 안의 INSERT INTO 구문 삭제
        4. 실행(F5)
