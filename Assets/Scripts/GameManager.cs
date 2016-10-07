using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	const float loadingTime = 3;

	int score = 0;

	public enum State {
		Display,
		Input,
	}

	Game game;

	State state = State.Display;

	UIManager uiManager;

	// Use this for initialization
	void Start () {
		uiManager = GetComponent<UIManager>();

		StartNewGame();

	}
	
	// Update is called once per frame
	void Update () {
		if (state != State.Input)
			return;

		CheckInput();
	}

	void StartNewGame() {
		state = State.Display;
		game = new Game(Difficulty.Easy);
		uiManager.ReceiveMark(game.CurrentMark);
		game.SetNextMark();
	}

	public void DisplayMark() {
		Debug.Log("display mark");
		// TODO マークを表示する
		if (game.HasNextMark()) {
			uiManager.ReceiveMark(game.CurrentMark);
			game.SetNextMark();
		} else {
			state = State.Input;
			game.ResetMarkCount();
		}
	}

	void CheckInput() {
		if (!IsEnableInput())
			return;

		Game.Result result = game.SendInput(GetInputMakrk());
		Debug.Log(result);
		if (result == Game.Result.Complete) {
			score++;
			StartCoroutine(NowLoading());
		} else if (result == Game.Result.Success) {
			score++;
		}
	}

	bool IsEnableInput() {
		return Input.GetKeyDown(KeyCode.K) || Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.D); 
	}

	Mark GetInputMakrk() {
		if (Input.GetKeyDown(KeyCode.K)) {
			return Mark.Circle;
		} else if (Input.GetKeyDown(KeyCode.J)) {
			return Mark.Cross;
		} else if (Input.GetKeyDown(KeyCode.F)) {
			return Mark.Square;
		} else if (Input.GetKeyDown(KeyCode.D)) {
			return Mark.Triangle;
		}
		throw new System.ArgumentException();
	}

	IEnumerator NowLoading() {
		yield return new WaitForSeconds(loadingTime);
		StartNewGame();
	}

}


public class Game {
	static int highScore;

	int markCount;
	List<Mark> marks;
	public Mark CurrentMark {
		get {
			return marks[markCount];
		}
	}

	Difficulty difficulty;

	public enum Result {
		Success,
		Fail,
		Complete,
	}


	public Game(Difficulty difficulty, int level = 0) {
		markCount = 0;
		this.difficulty = difficulty;
		marks = GenereateRandomMarks(this.difficulty);
	}

	public List<Mark> GenereateRandomMarks(Difficulty difficulty) {

		var list = new List<Mark>() {
			Mark.Circle,
			Mark.Cross,
			Mark.Triangle,
			Mark.Square
		};


		// TODO 問題生成実装方法を考える
		return list;
	}

	public Result SendInput(Mark inputMark) {
		if (!CheckMarkCorrection(inputMark))
			return Result.Fail;

		markCount++;
		if (markCount >= marks.Count)
			return Result.Complete;
		return Result.Success;
	}

	public bool CheckMarkCorrection(Mark inputMark) {
		return marks[markCount] == inputMark;
	}

	public void SetNextMark() {
		markCount++;
	}

	public bool HasNextMark() {
		return markCount < marks.Count;
	}

	public void ResetMarkCount() {
		markCount = 0;
	}

}


