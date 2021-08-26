new Vue({
    el: "#app",
    data: {
        selectedCandidates: [],
        errorMessage: null,
        verificationCodeErrorMessage: null,
        heading: "Welcome to voting app!",
        section: "welcome",
        userId: null,
        name: null,
        modal: false
    },
    methods: {
        submitDetails() {
            this.errorMessage = null

            fetch("/Users/CheckTel", {
                method: "POST",
                body: JSON.stringify({ tel: this.$refs["tel"].value }),
                headers: {
                    "Content-Type": "application/json"
                }
            })
                .then(response => response.json())
                .then(data => {
                    if (data.statusCode == 200) {
                        this.name = data.userName
                        this.userId = data.userId
                        this.modal = true
                    } else if (data.statusCode == 400) {
                        this.errorMessage = "Phone number cannot be empty."
                    } else if (data.statusCode == 401) {
                        this.errorMessage = "User not found."
                    }
                })
        },
        closeModal() {
            this.modal = false
            this.verificationCodeErrorMessage = null
        },
        submitVerificationCode() {
            fetch("/Users/CheckVerificationCode", {
                method: "POST",
                body: JSON.stringify({
                    userId: this.userId,
                    verificationCode: this.$refs["verification-code"].value
                }),
                headers: {
                    "Content-Type": "application/json"
                }
            })
                .then(response => response.json())
                .then(data => {
                    if (data.statusCode == 200) {
                        this.modal = false
                        this.verificationCodeErrorMessage = null
                        this.heading = "Please choose your candidate(s)"
                        this.section = "candidates"
                    } else if (data.statusCode == 400) {
                        this.verificationCodeErrorMessage = "Verification code cannot be empty."
                    } else if (data.statusCode == 401) {
                        this.verificationCodeErrorMessage = "Wrong verification code."
                    }
                })
        },
        toggleCandidate(e, candidateId) {
            if (this.selectedCandidates.includes(candidateId)) {
                this.selectedCandidates = this.selectedCandidates.filter(i => i !== candidateId)
                e.target.parentElement.classList.remove("selected")
            } else {
                this.selectedCandidates.push(candidateId)
                e.target.parentElement.classList.add("selected")
            }
        },
        sendVotes() {
            this.errorMessage = null

            fetch("/Votes/Create", {
                method: "POST",
                body: JSON.stringify(this.selectedCandidates),
                headers: {
                    "Content-Type": "application/json"
                }
            })
                .then(response => response.json())
                .then(data => {
                    if (data == 200) {
                        this.heading = `Thank you for voting, ${this.name}!`
                        this.section = "thank-you"
                    } else if (data == 400) {
                        this.errorMessage = "No candidate selected"
                    }
                })
        },
        home() {
            this.heading = "Welcome to voting app!"
            this.section = "welcome"
            this.name = null
            this.selectedCandidates = []
        }
    }
})