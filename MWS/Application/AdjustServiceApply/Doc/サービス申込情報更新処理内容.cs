<<< サービス申込情報更新処理内容 >>>

///////////////////////////////////////////////////
// メイン処理
//
Sub メイン処理

	call 顧客利用情報作成

	call サービス申込情報更新

End SUB


///////////////////////////////////////////////////
// 顧客利用情報作成
// ※朝バッチ内のAuto_Create_Data処理の代用
//
Function 顧客利用情報作成

	[WW伝票参照ビュー抽出]から受注承認日が前回同期日時以降の伝票の取得
	※1-1_Sel_V_CHECK.sql

	Loop 伝票
		IF 製品管理情報[T_PRODUCT_CONTROL]に伝票.ユーザー顧客IDに対するレコードが存在するか？
		※1-2_Sel_MwsID_Chk.sql
		Yes
			ログ出力 "WW伝票商品ID【XXXX】MWSID発行済み"

			IF MWSコードマスタ[M_CODE]に伝票.商品コードに対するレコードが存在するか？
			※1-3_Sel_M_CODE_Chk.sql
			Yes
				IF 月額課金用サービスかどうか？
				Yes
					IF [V_CUSTOMER]に伝票.ユーザー顧客IDに対するレコードが存在するか？
					※1-4_Sel_V_CUSTOMER.sql
					Yes
						// 顧客マスタ参照ビューの登録カード回収日がnullの場合、ライセンス発行可能フラグに=0、その他はライセンス発行可能フラグ=1
						ログ出力 "ライセンス発行可能フラグ=XXXX"
						ログ出力 "WW伝票担当者ID=XXXX"

						IF [社員マスタ参照ビュー]に営業担当のレコードが存在するか？
						No
							ログ出力 "警告「社員マスタ参照ビューに営業担当者ID(XXXX)が存在しません。」"
						EndIF

						IF WW伝票の販売先顧客IDとユーザー顧客IDが同じかどうか？
						Yes
							ログ出力 "WW伝票の販売先顧客IDとユーザー顧客IDが同じです。(XXXX)"
						No
							IF [販売店情報参照ビュー]に伝票.販売先顧客IDのレコードが存在するか？
							※1-5_Sel_V_STORE_INFORMATION.sql
							No
								ログ出力 "警告「WW伝票の販売先顧客ID({0})が販売店情報に存在しませんでした。」
							EndIF
						EndIF

						ログ出力 "WW伝票申込種別=XXXX"

						IF 顧客管理基本情報[T_CUSTOMER_FOUNDATIONS]に伝票.ユーザー顧客IDに対するレコードが存在するか？
						※1-6_Sel_T_CUSTOMER_FOUNDATIONS.sql
						No
							顧客管理基本情報[T_CUSTOMER_FOUNDATIONS]に顧客を登録
							ログ出力 "伝票No：XXXX、商品コード：XXXXのデータを顧客管理基本に登録しました。（顧客ID：XXXX 顧客名：XXXX 営業担当者ID：XXXX 販売店（使用料請求先コード / 販売拠点コード）：XXXX XXXX  XXXX"
						EndIF

						IF 顧客管理利用情報[T_CUSSTOMER_USE_INFOMATION]に顧客・サービスが存在するか？
						※1-7_Sel_T_CUSSTOMER_USE_INFOMATION.sql
						No
							IF 先月初日から当日までの申込データ[T_APPLICATION_DATA]に顧客・サービスが存在するか
							※1-8_Sel_T_APPLICATION_DATA_chk.sql
							Yes
								ログ出力 "伝票No：XXXX、商品コード：XXXXのデータは既に申込情報に存在しているため処理をスキップしました。（顧客ID：XXXX 顧客名：XXXX サービス種別ID：XXXX サービス種別名：XXXX サービスID：XXXX サービス名：XXXX）"
							No
								// 利用期間：当日～翌月末日
								// ※ただし、本処理で既に顧客管理基本情報[T_CUSTOMER_FOUNDATIONS]に顧客を登録済の場合には利用期間にNULLを指定←処理の意図が不明
								顧客管理利用情報[T_CUSSTOMER_USE_INFOMATION]に顧客・サービスを登録

								IF 利用開始日がNULL
								Yes
									ログ出力 "警告「伝票No：XXXX、商品コード：XXXXのデータを顧客管理利用情報に登録しました。（顧客ID：XXXX 顧客名：XXXX サービス種別ID：XXXX サービス種別名：XXXX サービスID：XXXX サービス名：XXXX)"
													 "※警告：登録されましたが利用期間が設定されていません。必ず利用期間を設定して下さい。利用期間が設定されるまではCouplerへの同期はされません。」"
								No
									ログ出力 "伝票No：XXXX、商品コード：XXXXのデータを顧客管理利用情報に登録しました。（顧客ID：XXXX 顧客名：XXXX サービス種別ID：XXXX サービス種別名：XXXX サービスID：XXXX サービス名：XXXX）"
								EndIF
							EndIF
						EndIF
					No
						ログ出力 "警告「COUPLER IDをもたないユーザーに「XXXX」のサービス伝票が起票されました。伝票No：XXXX、商品コード：XXXX（顧客ID：XXXX）」"
					EndIF
				No
					// 処理必要なし  一括販売用 ※PC安心サポートなど
					// 朝バッチでは一括販売用のサービスも顧客利用情報に追加していたので、登録する必要のないサービスが登録されていた
					ログ出力 "伝票No：XXXX、商品コード：XXXXのデータは一括販売用サービスなので、処理をスキップしました。（顧客ID：XXXX サービス種別ID：XXXX サービス種別名：XXXX サービスID：XXXX サービス名：XXXX）"
				EndIF
			No
				ログ出力 "警告「商品ID【XXXX】がコードマスター(M_CODE)に存在しません。」"
			EndIF
		No
			ログ出力 "警告「WW伝票参照ビュー：伝票No【XXXX】顧客ID【XXXX】※T_PRODUCT_CONTROLに顧客IDが存在しません。」"

			IF [MWSコードマスタ]に伝票.商品コードに対するレコードが存在するか？
			※1-3_Sel_M_CODE_Chk.sql
			Yes
				IF [V_CUSTOMER]に伝票.ユーザー顧客IDに対するレコードが存在するか？
				※1-4_Sel_V_CUSTOMER.sql
				Yes
					ログ出力 "警告「COUPLER IDをもたないユーザーに「XXXX」のサービス伝票が起票されました。伝票No：XXXX、商品コード：XXXX（顧客ID：XXXX 顧客名：XXXX）」"
				No
					ログ出力 "警告「COUPLER IDをもたないユーザーに「XXXX」のサービス伝票が起票されました。伝票No：XXXX、商品コード：XXXX（顧客ID：XXXX ）」"
				EndIF
			No
				ログ出力 "警告「商品ID【XXXX】がコードマスター(M_CODE)に存在しません。」"
			EndIF
		EndIF

		例外
			ログ出力 "例外発生「XXXX 伝票No：XXXX、商品コード：XXXX 顧客ID：XXXX」"
		End例外

	Loop End

End Function


///////////////////////////////////////////////////
// サービス申込情報更新
//
Function サービス申込情報更新

	ログ出力 "CHARLIEDBから基本機能パック取得開始 【PCA商品区分 = 200】"

	IF 基本機能パック 商品コード、サービス種別ID、サービスIDの取得できたか？
	※2-1_基本機能パック.sql
	Yes
		ログ出力 "CHARLIEDBから基本機能パック取得終了 【商品ID = XXXX / サービス種別ID = XXXX / サービスID = XXXX】"
	No
		ログ出力 "CHARLIEDBから基本機能パック取得終了 【商品ID = / サービス種別ID = / サービスID = 】"
	EndIF

	前回同期日時[FILE_CREATEDATE]の取得
	※2-2_Sel_T_FILE_CREATEDATE_利用情報.sql

	サービス申込情報[T_MWS_APPLY]から利用申込サービスの取得
	// 条件：システム反映フラグ=OFF AND 申込種別=利用申込 AND 顧客No=MWSユーザー
	※2-3_サービス申込情報-利用申込.sql

	IF サービス申込情報に利用申込サービスが存在する
	Yes
		顧客管理利用情報[T_CUSSTOMER_USE_INFOMATION]から利用申込サービスの取得
		// 条件：基本サービス以外 AND 課金対象外フラグ=OFF AND 利用開始日<>NULL AND 利用期限日=NULL AND 利用期間に翌月末日が含まれる AND 更新日時 > 前回同期日時
		※2-4_顧客利用情報-利用申込.sql

		IF 顧客管理利用情報に利用申込サービスが存在する
		Yes
			Loop 顧客管理利用情報の利用申込サービス
				サービス申込情報の利用申込サービスから顧客管理利用情報の利用申込サービスを取得
				IF 取得レコードが存在する
					Yes
						サービス申込情報のシステム反映フラグにONを設定して更新
						ログ出力 "利用申込情報システム反映フラグ設定（顧客ID：XXXX サービスID：XXXX 申込日時：XXXX）"
				EndIF
			Loop End
		EndIF
	EndIF

	サービス申込情報[T_MWS_APPLY]から解約申込サービスの取得
	// 条件：システム反映フラグ=OFF AND 申込種別=解約申込 AND 顧客No=MWSユーザー
	※2-5_サービス申込情報-解約申込.sql

	IF サービス申込情報に解約申込サービスが存在する
	Yes
		顧客管理利用情報[T_CUSSTOMER_USE_INFOMATION]から解約申込サービスの取得
		// 条件：基本サービス以外 AND 課金対象外フラグ=ON AND 課金終了日<>NULL AND 利用終了日=今月末日 AND 更新日時 > 前回同期日時
		※2-6_顧客利用情報-解約申込.sql		

		IF 顧客管理利用情報に解約申込サービスが存在する
		Yes
			Loop 顧客管理利用情報の解約申込サービス
				サービス申込情報の利用申込サービスから顧客管理利用情報の解約申込サービスを取得
				IF 取得レコードが存在する
					Yes
						サービス申込情報のシステム反映フラグにONを設定して更新
						ログ出力 "解約申込情報システム反映フラグ設定（顧客ID：XXXX サービスID：XXXX 申込日時：XXXX）"
				EndIF
			Loop End
		EndIF
	EndIF

	前回同期日時[FILE_CREATEDATE]の追加
	※2-7_InsertInto_T_FILE_CREATEDATE_利用情報.sql

End Function
