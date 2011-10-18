function QuestionEditor() {

    var questionText = '';
    var answerOptions = new Array();

    var me = this;

    this.QuestionText = questionText;
    this.AnswerOptions = answerOptions;

    
        //Find needed resources from html
        var questionTxtBox = $('#txtQuestion');
        var answerOptionsBox = $('#AnswerOptions');
        var answerOptionTemplate = $('#answerOptionEditTemplate');

        wireEvents();

        QuestionEditor.prototype.CreateQuestion = function () {
            getQuestionText();
            getAnswerOptions();
            sendQuestionToServer();
        };

    QuestionEditor.prototype.AddAnswerOption = function () {

        answerOptionTemplate.tmpl().appendTo(answerOptionsBox);

    };

    function getQuestionText() {


        var questionTxt = questionTxtBox.text();
        me.QuestionText = questionTxt;

    }


    function getAnswerOptions() {

        var options = answerOptionsBox.find('.answerOption');
        options.each(function () {
            var id = $(this).attr('id');
            var answerTxt = $(this).find('.txtAnswerOption').text();
            var correctAnswer = $(this).find('.cbxCorrect').attr('checked');
            me.AnswerOptions.push({ 'id': id, 'text': answerTxt, 'correct': correctAnswer });
        });
    }

    function removeAnswerOption(optionBtn) {

        optionBtn.parent().remove();

    }

    function wireEvents() {
        answerOptionsBox.find('.removeAnswerBtn').live('click', function ()
        { removeAnswerOption($(this))});

        $('#btnAddAnswer').bind('click', function () { me.AddAnswerOption(); return false; });


    }
    
    
    function sendQuestionToServer() {

        $.ajax({
            url: "AdminHome/CreateQuestion",
            type: "POST",
            data: { questionTxt: me.QuestionText, answerOptions: me.AnswerOptions },
            datatype: "json",
            success: function (result) {
                alert(result);

            },
            failure: function (result) {
                alert(result);
            }
        });
        
    }


}