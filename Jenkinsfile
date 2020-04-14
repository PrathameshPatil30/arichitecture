//https://plugins.jenkins.io/bitbucket-push-and-pull-request/
properties([
    pipelineTriggers([
        [
            $class: 'BitBucketPPRTrigger',
            triggers : [
                [
                    $class: 'BitBucketPPRPullRequestTriggerFilter',
                    actionFilter: [$class: 'BitBucketPPRPullRequestMergedActionFilter']
                ],
				[
					$class: 'BitBucketPPRRepositoryTriggerFilter',
					actionFilter: [
						$class: 'BitBucketPPRRepositoryPushActionFilter',
						triggerAlsoIfNothingChanged: true,
						triggerAlsoIfTagPush: false,
						allowedBranches: 'master'
					]
				]
            ]
        ]
    ])
])

version = ''
projectVersion = ''
commit_id = ''
sourceBranch = 'master'
targetBranch = 'master'
branchname = 'r1-clinical-document-api'
giturl = "git@bitbucket.org:r1rcm/${branchname}.git"
gitCredentialsId = 'git-r1rcm-com'
	
	pipeline {
	   agent any
		options {
			timestamps()
		}
		environment {
			SONAR_HOME = tool name: 'SonarMSCore', type: 'hudson.plugins.sonar.MsBuildSQRunnerInstallation'
		}
		parameters { 
			string(name: 'FROM_BRANCH', defaultValue: '', description: 'Manual build against specific branch (blank for default):')
			string(name: 'FROM_TAG', defaultValue: '', description: 'Manual build against specific tag (blank for default):')
		}
	   stages {
			stage('Clean Workspace') {
			   steps{
				 cleanWs() 
			   }
			}
			stage ('Pull request') {
				when { expression { is_pullrequest() } }
					stages {
						stage ('Pull Request - Pull Source Branch') {
							steps {
								script { 
									commit_id = get_commit_id()
									bitbucketStatusNotify(
										buildState: 'INPROGRESS',
										buildKey: 'pullrequest',
										buildName: 'Pullrequest',
										buildDescription: 'Starting the pull request build.',
										repoSlug: branchname,
										commitId: "${commit_id}")

									echo 'This path is only executed on PRs'
								
									sourceBranch = "${BITBUCKET_SOURCE_BRANCH}"
									targetBranch = "${BITBUCKET_TARGET_BRANCH}"

									echo "sourceBranch: ${sourceBranch}"
									echo "targetBranch: ${targetBranch}"
								}
								git branch: sourceBranch, credentialsId: gitCredentialsId, url: giturl
							}
						}
						stage ('Pull Request - Merge with Target') {
							steps {
								checkout([
									$class: 'GitSCM',
									branches: [[name: '*/' + sourceBranch]],
									doGenerateSubmoduleConfigurations: false,
									extensions: [[
										$class: 'PreBuildMerge',
										options: [
											mergeRemote: 'origin',
											mergeTarget: targetBranch,
											fastForwardMode: 'FF',
											mergeStrategy: 'recursive'
										]
									]],
									userRemoteConfigs: [[
										credentialsId: gitCredentialsId,
										url: giturl
									]]
								])
							}
						}
						stage ('Pull Request - Post-merge - Build') {
							steps {
								script {
									get_version()
								}
								script {
									run_build()
								}
							}
						}
						stage ('Pull Request - Post-merge - Test') {
							steps {
								unit_test()
							}
						}
						stage ('Pull Request - Wrap-up') {
							steps {
								bitbucketStatusNotify(
									buildState: 'SUCCESSFUL',
									buildKey: 'pullrequest',
									buildName: 'Pullrequest',
									repoSlug: branchname,
									commitId: "${commit_id}",
									buildDescription: 'Finished pullrequest.')
							}
						}
					}
			}
			stage('Regular Build'){
				when { expression { !is_pullrequest() } }
				stages {
					stage('Checkout Code') {
						steps {
							script {
								sourceBranch = get_sourceBranch()
								echo "Checking out: ${sourceBranch}"
							}

							checkout([
								$class: 'GitSCM',
								branches: [[name: sourceBranch]],
								doGenerateSubmoduleConfigurations: false,
								userRemoteConfigs: [[
									credentialsId: gitCredentialsId,
									url: giturl
								]]
							])

							script {
								commit_id = get_commit_id()
								echo "commit_id: ${commit_id}"				
							}
							
							bitbucketStatusNotify(
							buildState: 'INPROGRESS',
							buildKey: 'build',
							buildName: 'Build',
							repoSlug: branchname,
							commitId: "${commit_id}",
							buildDescription: 'Starting the build.')
						}
					}
					stage('Restore Packages') {
						steps {
							restore_package()					
						}
					}
					stage('Clean Build') {
					   steps {
							clean_build()
					   }
					}
					stage ('Get Version') {
						steps {
							get_version()
						}
					}
					stage ('Tag') {
						when { 
							expression { sourceBranch =~ 'master' }
							not { triggeredBy cause: "UserIdCause" } }
						steps {
							tag_commit()
						}
					}
					stage('Build') {
					   steps {
							run_build()
					   }
					}
					stage('Test: Unit Test'){
						steps {
							unit_test()
						}
					}
					stage ('Static Code Analysis') {
						steps {
							static_code_analysis()
						}
					}
					stage ('Archive') {
						steps {
							script {
								def artifactLocation = 'build\\ClinicalDocumentAPI_Published'
								def artifactName = "ClinicalDocumentAPI_${projectVersion}"
								zip archive: true, dir: "${artifactLocation}", glob: '', zipFile: "${artifactName}.zip"
								
								def server = Artifactory.server 'artifactory-1'
									def uploadSpec = """{
											"files": [
											{
												"pattern": "ClinicalDocumentAPI*.zip",
												"target": "build-artifacts/${env.JOB_NAME}/${projectVersion}/"
											}
										]
									}"""
								server.upload spec: uploadSpec, failNoOp: true
								echo "Artifact successfully uploaded"
							}
						}
					}
				}
			}			
	    }
		post {		
			always {
				echo 'Build Finished'
			}
			success {
				script {
					echo 'Build was successful'
					key = is_pullrequest() ? 'pullrequest' : 'build'
					name = is_pullrequest() ? 'Pullrequest' : 'Build'
					description = is_pullrequest() ? 'Pull-request successful' : 'Build successful'

					bitbucketStatusNotify(
						buildState: 'SUCCESSFUL',
						buildKey: key,
						buildName: name,
						repoSlug: branchname,
						commitId: "${commit_id}",
						buildDescription: description)

					notify_recipients()
				}
			}
			fixed {
				echo 'Build is now stable'
				notify_recipients()
			}
			changed {
				echo 'Build status changed'
			}
			aborted {
				echo 'Build was manually aborted'
				notify_recipients()
			}
			failure {
				script {
					echo "Build Failed"
					key = is_pullrequest() ? 'pullrequest' : 'build'
					name = is_pullrequest() ? 'Pullrequest' : 'Build'
					description = is_pullrequest() ? 'Pull-request failed' : 'Build failed'

					bitbucketStatusNotify(
						buildState: 'FAILURE',
						buildKey: key,
						buildName: name,
						repoSlug: branchname,
						commitId: "${commit_id}",
						buildDescription: description)

					notify_recipients()
				}
			}
			unstable {
				echo 'Build is Unstable'
				notify_recipients()
			}
		}	
	}
	
def get_sourceBranch() {
	if (params.FROM_BRANCH != '') {
		return "*/${params.FROM_BRANCH}"
	}

	if (params.FROM_TAG != '') {
		return "refs/tags/${params.FROM_TAG}"
	}

	// detect branch from environment
	def gitBranch = env.GIT_BRANCH

	// fall-back to master
	if (gitBranch == null) {
		gitBranch = 'master'
	}
	gitBranch = gitBranch.replace('origin/', '')
	echo "gitBranch: ${gitBranch}"

	return "*/${gitBranch}"
}

def restore_package(){
	def code = bat label: '', returnStatus: true, script: 'dotnet restore R1.ClinicalDocumentAPI.sln --configfile=Nuget.Config.xml'
	echo "Windows batch scripts exit code: ${code}"
	if (code != 0) {
		error('Restore failed')
	}
}

def clean_build(){
	def code = bat label: '', returnStatus: true, script: 'dotnet clean'
	echo "Windows batch scripts exit code: ${code}"
	if (code != 0) {
		error('Clean Build failed')
	}	
}

def run_build(){
	//Run Build
	def code = bat label: '',returnStatus: true, script: 'dotnet build -c Release'
	echo "Windows batch scripts exit code: ${code}"
	if (code != 0) {
		error('Build failed')
	}
	
	//Create Build Folder if it doesn't exist
	def code_check = bat label: '', script: 'if exist build ( echo build folder exists ) else ( mkdir build && echo build folder created)'
	
	//publish build
	def code_pub = bat label: '', returnStatus: true, script: 'dotnet publish -c Release --no-build -o build\\ClinicalDocumentAPI_Published'
	if (code_pub != 0) {
		error('Publish Failed')
	}
	else{
		echo "publish successful at location ${env:WORKSPACE}\\build\\ClinicalDocumentAPI_Published"
	}
}

def unit_test(){
	def code = bat label: '', returnStatus: true, script: 'dotnet test R1.ClinicalDocumentAPI.sln'
	echo "Windows batch scripts exit code: ${code}"
	if (code != 0) {
		error('Unit Test failed')
	}
	def report = bat label: '', returnStatus: true, script: 'dotnet test R1.ClinicalDocumentAPI.sln --logger \"trx;LogFileName=UnitTests.trx\" --no-build'
	echo "Windows batch scripts exit code: ${code}"
	if (report != 0) {
		error('Unit Test Report Generation Failed')
	}
	step([$class: 'MSTestPublisher', testResultsFile:"**/TestResults/UnitTests.trx", failOnError: true, keepLongStdio: true])			
}

def static_code_analysis() {
	def sonar_url = "https://sonarqube.r1rcm.com"
	def sonar_login = "789021dd02e7193781999538be3e40686cac2246"

	echo "SONAR_HOME: ${env.SONAR_HOME}"

	def code = bat label: '', returnStatus: true, script: "dotnet ${SONAR_HOME}\\SonarScanner.MSBuild.dll begin /k:ClinicalDocumentAPI_Jenkins /n:ClinicalDocumentAPI /d:sonar.host.url=\"${sonar_url}\" /d:sonar.login=\"${sonar_login}\" /d:sonar.cs.dotcover.reportsPaths=\"clinicalDocumentAPI-coverage-report.html\""
	if (code != 0) {
		error('Failed to begin static code analysis')
	}

	code = bat label: '', returnStatus: true, script: 'dotnet build --no-incremental R1.ClinicalDocumentAPI.sln'
	if (code != 0) {
		error('Failed to rebuild for static code analysis')
	}

	code = bat label: '', returnStatus: true, script: 'dotcover cover dot-cover.xml --TargetExecutable=\"C:\\Program Files\\dotnet\\dotnet.exe\"'
	// code = bat label: '', returnStatus: true, script: 'dotcover dotnet dot-cover.xml'
	if (code != 0) {
		error('Failed to run dotcover')
	}

	code = bat label: '', returnStatus: true, script: "dotnet ${SONAR_HOME}\\SonarScanner.MSBuild.dll end /d:sonar.login=\"${sonar_login}\""
	if (code != 0) {
		error('Failed to end static code analysis run')
	}
}

def get_version() {
	version = readFile file: "version"
	projectVersion = version.trim() + ".${BUILD_NUMBER}"
	echo "Project version ${projectVersion}"	
	return projectVersion
}
def tag_commit() {
	echo "attempt to tag commit \"${version}\""

	def suffix = env.BITBUCKET_TARGET_BRANCH != null ? '-rc' : ''
	def tagName = "v${version}${suffix}"

	if (!is_webhook() || is_pullrequest()) {
		echo "skip step"
		return
	}

	if (tag_exists(tagName)) {
		def describe = bat(returnStdout: true, script: '@echo off & git describe').trim()
		describe = describe.substring(0, describe.lastIndexOf('-'))
		tagName = describe
	}

	sshagent([gitCredentialsId]) 
	{
		bat label: 'Generate tag', script: "git tag -a ${tagName} -m \"${version}\""
		bat label: 'Push tag', script: "git push origin ${tagName}"
	}
}

def tag_exists(tag) {
	try {
		def tagHash = bat(returnStdout: true, script: "@echo off & git rev-parse ${tag}").trim()
		return tagHash != ""
	}
	catch (err) {
		// expected failure is "fatal: ambiguous argument '${tag}': unknown revision or path not in the working tree."
		echo "error: ${err}"
		return false
	}
}

def is_webhook() {
	return env.BITBUCKET_SOURCE_BRANCH
}

def is_pullrequest() {
	return env.BITBUCKET_PULL_REQUEST_ID
}

def get_commit_id() {
	if (is_webhook() && is_pullrequest()) {
		def payload = readJSON file: '', text: env.BITBUCKET_PAYLOAD
		echo "payload: ${payload}"

		def hash = payload['pullrequest']['source']['commit']['hash']
		return hash
	}

	def gitCommit = bat(returnStdout: true, script: '@echo off & git rev-parse HEAD').trim()
	echo "gitCommit: ${gitCommit}"
	return gitCommit
}

def notify_recipients() {
    emailext (
            to: 'ppatil2@r1rcm.com,vs1@r1rcm.com,ssarang@r1rcm.com',
			recipientProviders: [culprits(), developers(), requestor()],
			compressLog: true,
            subject: '$PROJECT_NAME - Build # $BUILD_NUMBER - $BUILD_STATUS!',
			body:  '''${SCRIPT, template="groovy-html.template"}'''
        )
}