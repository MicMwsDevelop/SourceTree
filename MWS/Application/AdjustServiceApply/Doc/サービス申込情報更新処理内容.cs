<<< �T�[�r�X�\�����X�V�������e >>>

///////////////////////////////////////////////////
// ���C������
//
Sub ���C������

	call �ڋq���p���쐬

	call �T�[�r�X�\�����X�V

End SUB


///////////////////////////////////////////////////
// �ڋq���p���쐬
// �����o�b�`����Auto_Create_Data�����̑�p
//
Function �ڋq���p���쐬

	[WW�`�[�Q�ƃr���[���o]����󒍏��F�����O�񓯊������ȍ~�̓`�[�̎擾
	��1-1_Sel_V_CHECK.sql

	Loop �`�[
		IF ���i�Ǘ����[T_PRODUCT_CONTROL]�ɓ`�[.���[�U�[�ڋqID�ɑ΂��郌�R�[�h�����݂��邩�H
		��1-2_Sel_MwsID_Chk.sql
		Yes
			���O�o�� "WW�`�[���iID�yXXXX�zMWSID���s�ς�"

			IF MWS�R�[�h�}�X�^[M_CODE]�ɓ`�[.���i�R�[�h�ɑ΂��郌�R�[�h�����݂��邩�H
			��1-3_Sel_M_CODE_Chk.sql
			Yes
				IF ���z�ۋ��p�T�[�r�X���ǂ����H
				Yes
					IF [V_CUSTOMER]�ɓ`�[.���[�U�[�ڋqID�ɑ΂��郌�R�[�h�����݂��邩�H
					��1-4_Sel_V_CUSTOMER.sql
					Yes
						// �ڋq�}�X�^�Q�ƃr���[�̓o�^�J�[�h�������null�̏ꍇ�A���C�Z���X���s�\�t���O��=0�A���̑��̓��C�Z���X���s�\�t���O=1
						���O�o�� "���C�Z���X���s�\�t���O=XXXX"
						���O�o�� "WW�`�[�S����ID=XXXX"

						IF [�Ј��}�X�^�Q�ƃr���[]�ɉc�ƒS���̃��R�[�h�����݂��邩�H
						No
							���O�o�� "�x���u�Ј��}�X�^�Q�ƃr���[�ɉc�ƒS����ID(XXXX)�����݂��܂���B�v"
						EndIF

						IF WW�`�[�̔̔���ڋqID�ƃ��[�U�[�ڋqID���������ǂ����H
						Yes
							���O�o�� "WW�`�[�̔̔���ڋqID�ƃ��[�U�[�ڋqID�������ł��B(XXXX)"
						No
							IF [�̔��X���Q�ƃr���[]�ɓ`�[.�̔���ڋqID�̃��R�[�h�����݂��邩�H
							��1-5_Sel_V_STORE_INFORMATION.sql
							No
								���O�o�� "�x���uWW�`�[�̔̔���ڋqID({0})���̔��X���ɑ��݂��܂���ł����B�v
							EndIF
						EndIF

						���O�o�� "WW�`�[�\�����=XXXX"

						IF �ڋq�Ǘ���{���[T_CUSTOMER_FOUNDATIONS]�ɓ`�[.���[�U�[�ڋqID�ɑ΂��郌�R�[�h�����݂��邩�H
						��1-6_Sel_T_CUSTOMER_FOUNDATIONS.sql
						No
							�ڋq�Ǘ���{���[T_CUSTOMER_FOUNDATIONS]�Ɍڋq��o�^
							���O�o�� "�`�[No�FXXXX�A���i�R�[�h�FXXXX�̃f�[�^���ڋq�Ǘ���{�ɓo�^���܂����B�i�ڋqID�FXXXX �ڋq���FXXXX �c�ƒS����ID�FXXXX �̔��X�i�g�p��������R�[�h / �̔����_�R�[�h�j�FXXXX XXXX  XXXX"
						EndIF

						IF �ڋq�Ǘ����p���[T_CUSSTOMER_USE_INFOMATION]�Ɍڋq�E�T�[�r�X�����݂��邩�H
						��1-7_Sel_T_CUSSTOMER_USE_INFOMATION.sql
						No
							IF �挎�������瓖���܂ł̐\���f�[�^[T_APPLICATION_DATA]�Ɍڋq�E�T�[�r�X�����݂��邩
							��1-8_Sel_T_APPLICATION_DATA_chk.sql
							Yes
								���O�o�� "�`�[No�FXXXX�A���i�R�[�h�FXXXX�̃f�[�^�͊��ɐ\�����ɑ��݂��Ă��邽�ߏ������X�L�b�v���܂����B�i�ڋqID�FXXXX �ڋq���FXXXX �T�[�r�X���ID�FXXXX �T�[�r�X��ʖ��FXXXX �T�[�r�XID�FXXXX �T�[�r�X���FXXXX�j"
							No
								// ���p���ԁF�����`��������
								// ���������A�{�����Ŋ��Ɍڋq�Ǘ���{���[T_CUSTOMER_FOUNDATIONS]�Ɍڋq��o�^�ς̏ꍇ�ɂ͗��p���Ԃ�NULL���w�聩�����̈Ӑ}���s��
								�ڋq�Ǘ����p���[T_CUSSTOMER_USE_INFOMATION]�Ɍڋq�E�T�[�r�X��o�^

								IF ���p�J�n����NULL
								Yes
									���O�o�� "�x���u�`�[No�FXXXX�A���i�R�[�h�FXXXX�̃f�[�^���ڋq�Ǘ����p���ɓo�^���܂����B�i�ڋqID�FXXXX �ڋq���FXXXX �T�[�r�X���ID�FXXXX �T�[�r�X��ʖ��FXXXX �T�[�r�XID�FXXXX �T�[�r�X���FXXXX)"
													 "���x���F�o�^����܂��������p���Ԃ��ݒ肳��Ă��܂���B�K�����p���Ԃ�ݒ肵�ĉ������B���p���Ԃ��ݒ肳���܂ł�Coupler�ւ̓����͂���܂���B�v"
								No
									���O�o�� "�`�[No�FXXXX�A���i�R�[�h�FXXXX�̃f�[�^���ڋq�Ǘ����p���ɓo�^���܂����B�i�ڋqID�FXXXX �ڋq���FXXXX �T�[�r�X���ID�FXXXX �T�[�r�X��ʖ��FXXXX �T�[�r�XID�FXXXX �T�[�r�X���FXXXX�j"
								EndIF
							EndIF
						EndIF
					No
						���O�o�� "�x���uCOUPLER ID�������Ȃ����[�U�[�ɁuXXXX�v�̃T�[�r�X�`�[���N�[����܂����B�`�[No�FXXXX�A���i�R�[�h�FXXXX�i�ڋqID�FXXXX�j�v"
					EndIF
				No
					// �����K�v�Ȃ�  �ꊇ�̔��p ��PC���S�T�|�[�g�Ȃ�
					// ���o�b�`�ł͈ꊇ�̔��p�̃T�[�r�X���ڋq���p���ɒǉ����Ă����̂ŁA�o�^����K�v�̂Ȃ��T�[�r�X���o�^����Ă���
					���O�o�� "�`�[No�FXXXX�A���i�R�[�h�FXXXX�̃f�[�^�͈ꊇ�̔��p�T�[�r�X�Ȃ̂ŁA�������X�L�b�v���܂����B�i�ڋqID�FXXXX �T�[�r�X���ID�FXXXX �T�[�r�X��ʖ��FXXXX �T�[�r�XID�FXXXX �T�[�r�X���FXXXX�j"
				EndIF
			No
				���O�o�� "�x���u���iID�yXXXX�z���R�[�h�}�X�^�[(M_CODE)�ɑ��݂��܂���B�v"
			EndIF
		No
			���O�o�� "�x���uWW�`�[�Q�ƃr���[�F�`�[No�yXXXX�z�ڋqID�yXXXX�z��T_PRODUCT_CONTROL�ɌڋqID�����݂��܂���B�v"

			IF [MWS�R�[�h�}�X�^]�ɓ`�[.���i�R�[�h�ɑ΂��郌�R�[�h�����݂��邩�H
			��1-3_Sel_M_CODE_Chk.sql
			Yes
				IF [V_CUSTOMER]�ɓ`�[.���[�U�[�ڋqID�ɑ΂��郌�R�[�h�����݂��邩�H
				��1-4_Sel_V_CUSTOMER.sql
				Yes
					���O�o�� "�x���uCOUPLER ID�������Ȃ����[�U�[�ɁuXXXX�v�̃T�[�r�X�`�[���N�[����܂����B�`�[No�FXXXX�A���i�R�[�h�FXXXX�i�ڋqID�FXXXX �ڋq���FXXXX�j�v"
				No
					���O�o�� "�x���uCOUPLER ID�������Ȃ����[�U�[�ɁuXXXX�v�̃T�[�r�X�`�[���N�[����܂����B�`�[No�FXXXX�A���i�R�[�h�FXXXX�i�ڋqID�FXXXX �j�v"
				EndIF
			No
				���O�o�� "�x���u���iID�yXXXX�z���R�[�h�}�X�^�[(M_CODE)�ɑ��݂��܂���B�v"
			EndIF
		EndIF

		��O
			���O�o�� "��O�����uXXXX �`�[No�FXXXX�A���i�R�[�h�FXXXX �ڋqID�FXXXX�v"
		End��O

	Loop End

End Function


///////////////////////////////////////////////////
// �T�[�r�X�\�����X�V
//
Function �T�[�r�X�\�����X�V

	���O�o�� "CHARLIEDB�����{�@�\�p�b�N�擾�J�n �yPCA���i�敪 = 200�z"

	IF ��{�@�\�p�b�N ���i�R�[�h�A�T�[�r�X���ID�A�T�[�r�XID�̎擾�ł������H
	��2-1_��{�@�\�p�b�N.sql
	Yes
		���O�o�� "CHARLIEDB�����{�@�\�p�b�N�擾�I�� �y���iID = XXXX / �T�[�r�X���ID = XXXX / �T�[�r�XID = XXXX�z"
	No
		���O�o�� "CHARLIEDB�����{�@�\�p�b�N�擾�I�� �y���iID = / �T�[�r�X���ID = / �T�[�r�XID = �z"
	EndIF

	�O�񓯊�����[FILE_CREATEDATE]�̎擾
	��2-2_Sel_T_FILE_CREATEDATE_���p���.sql

	�T�[�r�X�\�����[T_MWS_APPLY]���痘�p�\���T�[�r�X�̎擾
	// �����F�V�X�e�����f�t���O=OFF AND �\�����=���p�\�� AND �ڋqNo=MWS���[�U�[
	��2-3_�T�[�r�X�\�����-���p�\��.sql

	IF �T�[�r�X�\�����ɗ��p�\���T�[�r�X�����݂���
	Yes
		�ڋq�Ǘ����p���[T_CUSSTOMER_USE_INFOMATION]���痘�p�\���T�[�r�X�̎擾
		// �����F��{�T�[�r�X�ȊO AND �ۋ��ΏۊO�t���O=OFF AND ���p�J�n��<>NULL AND ���p������=NULL AND ���p���Ԃɗ����������܂܂�� AND �X�V���� > �O�񓯊�����
		��2-4_�ڋq���p���-���p�\��.sql

		IF �ڋq�Ǘ����p���ɗ��p�\���T�[�r�X�����݂���
		Yes
			Loop �ڋq�Ǘ����p���̗��p�\���T�[�r�X
				�T�[�r�X�\�����̗��p�\���T�[�r�X����ڋq�Ǘ����p���̗��p�\���T�[�r�X���擾
				IF �擾���R�[�h�����݂���
					Yes
						�T�[�r�X�\�����̃V�X�e�����f�t���O��ON��ݒ肵�čX�V
						���O�o�� "���p�\�����V�X�e�����f�t���O�ݒ�i�ڋqID�FXXXX �T�[�r�XID�FXXXX �\�������FXXXX�j"
				EndIF
			Loop End
		EndIF
	EndIF

	�T�[�r�X�\�����[T_MWS_APPLY]������\���T�[�r�X�̎擾
	// �����F�V�X�e�����f�t���O=OFF AND �\�����=���\�� AND �ڋqNo=MWS���[�U�[
	��2-5_�T�[�r�X�\�����-���\��.sql

	IF �T�[�r�X�\�����ɉ��\���T�[�r�X�����݂���
	Yes
		�ڋq�Ǘ����p���[T_CUSSTOMER_USE_INFOMATION]������\���T�[�r�X�̎擾
		// �����F��{�T�[�r�X�ȊO AND �ۋ��ΏۊO�t���O=ON AND �ۋ��I����<>NULL AND ���p�I����=�������� AND �X�V���� > �O�񓯊�����
		��2-6_�ڋq���p���-���\��.sql		

		IF �ڋq�Ǘ����p���ɉ��\���T�[�r�X�����݂���
		Yes
			Loop �ڋq�Ǘ����p���̉��\���T�[�r�X
				�T�[�r�X�\�����̗��p�\���T�[�r�X����ڋq�Ǘ����p���̉��\���T�[�r�X���擾
				IF �擾���R�[�h�����݂���
					Yes
						�T�[�r�X�\�����̃V�X�e�����f�t���O��ON��ݒ肵�čX�V
						���O�o�� "���\�����V�X�e�����f�t���O�ݒ�i�ڋqID�FXXXX �T�[�r�XID�FXXXX �\�������FXXXX�j"
				EndIF
			Loop End
		EndIF
	EndIF

	�O�񓯊�����[FILE_CREATEDATE]�̒ǉ�
	��2-7_InsertInto_T_FILE_CREATEDATE_���p���.sql

End Function
