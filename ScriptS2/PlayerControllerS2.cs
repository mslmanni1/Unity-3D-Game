using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerS2 : MonoBehaviour {
	public float speed = 15;
	public float init_speed=15;
	InputDirection inputDirection;
	Vector3 mousePos;
	bool activeInput;
	Position standPosition;
	Position fromPosition;
	Vector3 xDirection;
	Vector3 moveDirection;
	CharacterController characterController;
	float jumpValue = 7;
	float gravity = 20;
	public bool canDoubleJump = true;
	bool doubleJump = false;
	bool isQuickMoving = false;
	float saveSpeed;
	float quickMoveDuration = 3;
	public float quickMoveTimeLeft;
	IEnumerator quickMoveCor;

	float magnetDuration = 15;
	public float magnetTimeLeft;
	IEnumerator magnetCor;
	public GameObject MagnetCollider;

	float shoeDuration = 10;
	public float shoeTimeLeft;
	IEnumerator shoeCor;

	float multiplyDuration = 10;
	public float multiplyTimeLeft;
	IEnumerator multiplyCor;

	//	public Text statusText;

	public Text Text_Magnet;
	public Text Text_Shoe;
	public Text Text_Star;
	public Text Text_Multiply;


	public GameObject road1;
	public GameObject road2;
	public GameObject start;



	//我把这个isRoll设成了global variable 在animationManager里
	//public bool isRoll = false;

	public static PlayerControllerS2 instance;
	private Animation animate;

	// Use this for initialization
	void Start () {
		instance = this;
		animate = GetComponent<Animation> ();
		characterController = GetComponent<CharacterController>();
		standPosition = Position.Middle;
		StartCoroutine (UpdateAction());
	}

	public void Play()
	{
		GameControllerS2.instance.isPause = false;
		GameControllerS2.instance.isPlay = true;
		StartCoroutine(UpdateAction());
	}

	IEnumerator UpdateAction()
	{
		while (GameAttributeS2.instance.life>0) {
			if (GameControllerS2.instance.isPlay && !GameControllerS2.instance.isPause) {
				GetInputDirection ();
				//PlayAnimation ();
				MoveLeftRight();
				MoveForward();
			}else{
				animate.Stop();
			}

			yield return 0;
		}
		speed = 0;
		GameControllerS2.instance.isPlay = false;
		AnimationManagerS2.instance.animationHandler = AnimationManagerS2.instance.PlayDead;
		iTween.MoveTo(gameObject, gameObject.transform.position - new Vector3(0, 0, 2), 2.0f);
		yield return new WaitForSeconds (3);
		UIcontrollerS2.instance.ShowRestartUI();
		UIcontrollerS2.instance.HidePauseUI();
//		while (GameAttribute.instance.life>0) {
//			if (GameController.instance.isPlay && !GameController.instance.isPause)
//			{
//				GetInputDirection ();
//				MoveLeftRight();
//				MoveForward ();	
//			} 
//			else{
//				animate.Stop();
//			}
//
//			yield return 0;
//		}
//		speed = 0;
//		GameController.instance.isPlay = false;
//		xDirection = Vector3.zero;
//		AnimationManager.instance.animationHandler = AnimationManager.instance.PlayDead;
//		iTween.MoveTo(gameObject, gameObject.transform.position - new Vector3(0, 0, 2), 2.0f);
//		yield return new WaitForSeconds (3);
//		Debug.Log ("restart");
//
//		UIController.instance.ShowRestartUI();
//		UIController.instance.HidePauseUI();
	}

	void MoveForward(){
		if(inputDirection == InputDirection.Down){
			AnimationManagerS2.instance.animationHandler = AnimationManagerS2.instance.PlayRoll;
		}
		if(characterController.isGrounded){
			moveDirection = Vector3.zero;
			if(AnimationManagerS2.instance.animationHandler != AnimationManagerS2.instance.PlayRoll &&
				AnimationManagerS2.instance.animationHandler != AnimationManagerS2.instance.PlayTurnLeft && 
				AnimationManagerS2.instance.animationHandler != AnimationManagerS2.instance.PlayTurnRight
			){
				AnimationManagerS2.instance.animationHandler = AnimationManagerS2.instance.PlayRun; 
			}

			if(inputDirection == InputDirection.Up){
				JumpUp();
				if(canDoubleJump){
					doubleJump = true;
				}
			}
		}else{

			if(inputDirection == InputDirection.Down){
				QuickGround();
			}

			if(inputDirection == InputDirection.Up){

				if(doubleJump){
					JumpDouble();
					doubleJump = false;
				}

			}


			if(AnimationManagerS2.instance.animationHandler != AnimationManagerS2.instance.PlayJumpUp
				&& AnimationManagerS2.instance.animationHandler != AnimationManagerS2.instance.PlayRoll
				&& AnimationManagerS2.instance.animationHandler != AnimationManagerS2.instance.PlayDoubleJump
			){
				//这里有问题，如果用这行的话左右跳会很奇怪，但是这个在空中停留的动作也不明显，所以注释掉了，免得影响左右跳
				//AnimationManagerS2.instance.animationHandler = AnimationManagerS2.instance.PlayJumpLoop;
			}

		}

		moveDirection.z = speed;
		moveDirection.y -= gravity * Time.deltaTime;
		characterController.Move((xDirection * 10 + moveDirection) * Time.deltaTime);
	}

	public void Reset()
	{
		speed = init_speed;
		inputDirection = InputDirection.NULL;
		activeInput = false;
		standPosition = Position.Middle;
		xDirection = Vector3.zero;
		moveDirection = Vector3.zero;
//		isRoll = false;
		canDoubleJump = false;
		isQuickMoving = false;
		quickMoveTimeLeft = 0;
		magnetTimeLeft = 0;
		shoeTimeLeft = 0;
		multiplyTimeLeft = 0;

		gameObject.transform.position = new Vector3(0, 0, -190);
		Camera.main.transform.position = new Vector3(0, 3, -194);

		AnimationManagerS2.instance.animationHandler = AnimationManagerS2.instance.PlayRun;

		var newRoad1 = Respawn("road1", road1, new Vector3(0, 0, 0));
		var newRoad2 = Respawn("road2", road2, new Vector3(0, 0, 100));
//		Respawn("start1", start1, new Vector3(0, 0, -60));
//		Respawn("start2", start2, new Vector3(0, 0, -0));
		if (FloorSetterS2.instance == null) {
			Debug.Log("floor none");
		}
		FloorSetterS2.instance.floorOnRunning = newRoad1;
		FloorSetterS2.instance.floorForward = newRoad2;
	}

	private GameObject Respawn(string name,GameObject prefab,Vector3 location)
	{
		var old = GameObject.Find(name);
		if (old != null)
		{
			Destroy(old);
			var newObj = Instantiate(prefab);
			newObj.name = name;
			newObj.transform.localPosition = location;
			return newObj;
		}
		return null;
	}


	void QuickGround(){
		moveDirection.y -= jumpValue*3;
	}

	void JumpDouble(){
		AnimationManagerS2.instance.animationHandler = AnimationManagerS2.instance.PlayDoubleJump;
		moveDirection.y += jumpValue*1.3f;
		
	}

	void JumpUp(){
		AnimationManagerS2.instance.animationHandler = AnimationManagerS2.instance.PlayJumpUp;
		moveDirection.y += jumpValue;
	}


	void MoveLeft(){

		if(standPosition != Position.Left){
			GetComponent<Animation>().Stop();
			AnimationManagerS2.instance.animationHandler = AnimationManagerS2.instance.PlayTurnLeft;

			xDirection = Vector3.left;

			if(standPosition == Position.Middle){
				standPosition = Position.Left;
				fromPosition = Position.Middle;
			}else if(standPosition == Position.Right){
				standPosition = Position.Middle;
				fromPosition = Position.Right;
			}
		}



	}



	void MoveRight(){

		if(standPosition != Position.Right){
			GetComponent<Animation>().Stop();
			AnimationManagerS2.instance.animationHandler = AnimationManagerS2.instance.PlayTurnRight;

			xDirection = Vector3.right;

			if(standPosition == Position.Middle){
				standPosition = Position.Right;
				fromPosition = Position.Middle;
			}else if(standPosition == Position.Left){
				standPosition = Position.Middle;
				fromPosition = Position.Left;
			}
		}


	}

	void MoveLeftRight(){

		if (inputDirection == InputDirection.Left){
			MoveLeft();
		}else if(inputDirection == InputDirection.Right){
			MoveRight();
		}

		if (standPosition == Position.Left){
			if(transform.position.x <= -1.7f){
				xDirection = Vector3.zero;
				transform.position = new Vector3(-1.7f, transform.position.y, transform.position.z);
			}
		}

		if(standPosition == Position.Middle){
			if(fromPosition == Position.Left){
				if(transform.position.x > 0){
					xDirection = Vector3.zero;
					transform.position = new Vector3(0, transform.position.y, transform.position.z);
				}
			}else if(fromPosition == Position.Right){
				if(transform.position.x < 0){
					xDirection = Vector3.zero;
					transform.position = new Vector3(0, transform.position.y, transform.position.z);
				}
			}
		}

		if (standPosition == Position.Right){
			if(transform.position.x >= 1.7f){
				xDirection = Vector3.zero;
				transform.position = new Vector3(1.7f, transform.position.y, transform.position.z);
			}
		}



	}

		void PlayAnimation()
		{
			if (inputDirection == InputDirection.Left) {
				AnimationManagerS2.instance.animationHandler = AnimationManagerS2.instance.PlayTurnLeft;	
			}else if(inputDirection==InputDirection.Right){
				AnimationManagerS2.instance.animationHandler = AnimationManagerS2.instance.PlayTurnRight;	
			}else if(inputDirection==InputDirection.Up){
				AnimationManagerS2.instance.animationHandler = AnimationManagerS2.instance.PlayJumpUp;	
			}else if(inputDirection == InputDirection.Down){
				AnimationManagerS2.instance.animationHandler = AnimationManagerS2.instance.PlayRoll;	
	
			}

		}

	private bool CanPlay()
	{
		return !GameControllerS2.instance.isPause && GameControllerS2.instance.isPlay;
	}


	public void QuickMove()
	{
		//如果协程已经创建，则停止
		if (quickMoveCor != null)
			StopCoroutine(quickMoveCor);

		//初始化协程
		quickMoveCor = QuickMoveCoroutine();

		//启动协程
		StartCoroutine(quickMoveCor);
	}

	IEnumerator QuickMoveCoroutine()
	{
		//初始剩余时间
		quickMoveTimeLeft = quickMoveDuration;

		//如果不在暴走
		if(!isQuickMoving)
			saveSpeed = speed;  //保存暴走之前的速度
		speed = 15;     //设置暴走速度
		isQuickMoving = true;   //正在暴走
		while (quickMoveTimeLeft>=0)    //倒计时
		{
			if(CanPlay())
				quickMoveTimeLeft -= Time.deltaTime;    //时间递减
			yield return null;
		}
		speed = saveSpeed;  //恢复暴走前的速度
		isQuickMoving = false;  //不在暴走
	}

	public void UseMagnet()
	{
		//如果协程已经创建，则停止
		if (magnetCor != null)
			StopCoroutine(magnetCor);

		//初始化协程
		magnetCor = MagnetCoroutine();

		//启动协程
		StartCoroutine(magnetCor);
	}

	IEnumerator MagnetCoroutine()
	{
		//初始剩余时间
		magnetTimeLeft = magnetDuration;

		//开启磁铁碰撞器
		MagnetCollider.SetActive(true);

		while (magnetTimeLeft >= 0) //倒计时
		{
			if(CanPlay())
				magnetTimeLeft -= Time.deltaTime;   //时间递减
			yield return null;
		}

		//关闭磁铁碰撞器
		MagnetCollider.SetActive(false);
	}

	public void UseShoe()
	{
		//如果协程已经创建，则停止
		if (shoeCor != null)
			StopCoroutine(shoeCor);

		//初始化协程
		shoeCor = ShoeCoroutine();

		//启动协程
		StartCoroutine(shoeCor);
	}

	IEnumerator ShoeCoroutine()
	{
		//初始剩余时间
		shoeTimeLeft = shoeDuration;

		//设置能双连跳
		PlayerControllerS2.instance.canDoubleJump = true;

		while (shoeTimeLeft >= 0)   //倒计时
		{
			if(CanPlay())
				shoeTimeLeft -= Time.deltaTime; //时间递减
			yield return null;
		}

		//取消双连跳
		PlayerControllerS2.instance.canDoubleJump = false;
	}

	public void Multiply()
	{
		//如果协程已经创建，则停止
		if (multiplyCor != null)
			StopCoroutine(multiplyCor);

		//初始化协程
		multiplyCor = MultiplyCoroutine();

		//启动协程
		StartCoroutine(multiplyCor);
	}

	IEnumerator MultiplyCoroutine()
	{
		//初始剩余时间
		multiplyTimeLeft = multiplyDuration;

		//更新游戏配置中的积分基数
		GameAttributeS2.instance.multiply = 2;

		//倒计时
		while(multiplyTimeLeft>=0)
		{
			if(CanPlay())
				multiplyTimeLeft -= Time.deltaTime; //时间递减
			yield return null;
		}

		//恢复游戏配置中的积分基数
		GameAttributeS2.instance.multiply = 1;
	}

	//获取输入 向量积反余弦 获得inputDirection的赋值
	void GetInputDirection()
	{
		inputDirection = InputDirection.NULL;
		if (Input.GetMouseButtonDown (0)) {
			activeInput = true;
			mousePos = Input.mousePosition;
		}
		if (Input.GetMouseButton (0) && activeInput) {
			Vector3 vec = Input.mousePosition - mousePos;
			if (vec.magnitude > 20)
			{
				var angleY = Mathf.Acos(Vector3.Dot(vec.normalized, Vector2.up)) * Mathf.Rad2Deg;
				var anglex = Mathf.Acos(Vector3.Dot(vec.normalized, Vector2.right)) * Mathf.Rad2Deg;
				if (angleY <= 45)
				{
					inputDirection = InputDirection.Up;
					AudioManagerS2.instance.PlaySlideAudio ();
				}
				else if(angleY >=135)
				{
					inputDirection = InputDirection.Down;
					AudioManagerS2.instance.PlaySlideAudio ();
				}
				else if (anglex <= 45)
				{
					inputDirection = InputDirection.Right;
					AudioManagerS2.instance.PlaySlideAudio ();
				}
				else if(anglex>=135)
				{
					inputDirection = InputDirection.Left;
					AudioManagerS2.instance.PlaySlideAudio ();
				}
				activeInput = false;
				Debug.Log(inputDirection);
			}

		}
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Translate (new Vector3(0, 0, speed*Time.deltaTime));
//		moveDirection.z = speed;
//		moveDirection.y -= gravity * Time.deltaTime;
//		characterController.Move((xDirection * 10 + moveDirection) * Time.deltaTime);
		UpdateItemTime();
	}

	private void UpdateItemTime()
	{
		if (Text_Magnet != null) {
			Text_Magnet.text = GetTime(magnetTimeLeft);
		}
		if (Text_Multiply != null) {
			Text_Multiply.text = GetTime(multiplyTimeLeft);
		}
		if (Text_Shoe != null) {
			Text_Shoe.text = GetTime(shoeTimeLeft);
		}
		if (Text_Star != null) {
			Text_Star.text = GetTime(quickMoveTimeLeft);
		}


		//
		//
		//
	}

	private string GetTime(float time)
	{
		//小于等于0，返回空
		if (time <= 0)
			return "";

		//获取文本显示
		return ((int)time + 1).ToString()+"s";
	}
}

//Already defined in scene 1 as a global namespace.
//public enum InputDirection
//{
//	NULL,
//	Left,
//	Right,
//	Up,
//	Down
//}

//Already defined in scene 1 as a global namespace.
//public enum Position{
//	Left,
//	Middle,
//	Right
//}