
test('CreateQuestionTest()', function() {
    var qEditor = new QuestionEditor();
   
    qEditor.CreateQuestion();
    ok(qEditor.QuestionText.length > 0,'Failed, no question text found');


});

test('AddAnswerOptionTest()', function () {
    var qEditor = new QuestionEditor();
    qEditor.AddAnswerOption();
    var options = $('.answerOption');

    ok(options.length >0, 'Failed, no added answer options found');

});


test('CreateQuestionWithAnswerTest()', function () {
    var qEditor = new QuestionEditor();
    qEditor.AddAnswerOption();
    var answer = $('.answerOption').first();
    answer.find('.txtAnswerOption').first().text('Test answer');
    answer.find('.cbxCorrect').first().attr('checked', true);
    qEditor.CreateQuestion();

    

    ok(qEditor.QuestionText.length >0, 'Question text:' + qEditor.QuestionText);
    ok(qEditor.AnswerOptions.length == 1,qEditor.AnswerOptions.toString());


});

test('GetQuestionTest()', function () {
    stop();
    var quiz = new Quiz();


    quiz.Host = 'localhost:53311';


    quiz.GetQuizStatus();
    quiz.GetQuestionFromServer();
    setTimeout()
    ok(quiz.CurrentQuestion.AnswerOptions.length > 0);


});

