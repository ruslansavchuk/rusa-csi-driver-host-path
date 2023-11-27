{{/*
Csi service account labels
*/}}
{{- define "csi-sa-labels" -}}
{{ include "csi-common-labels" . }}
app.kubernetes.io/component: csi-service-account
{{- end }}

{{/*
Csi controller labels
*/}}
{{- define "csi-cr-labels" -}}
{{ include "csi-common-labels" . }}
app.kubernetes.io/component: csi-cluster-role
{{- end }}

{{/*
Csi controller service labels
*/}}
{{- define "csi-controller-service-labels" -}}
{{ include "csi-common-labels" . }}
app.kubernetes.io/component: csi-controller-service
{{- end }}

{{/*
Csi controller labels
*/}}
{{- define "csi-controller-labels" -}}
{{ include "csi-common-labels" . }}
app.kubernetes.io/component: csi-controller
{{- end }}

{{/*
Csi controller labels
*/}}
{{- define "csi-controller-selectors" -}}
{{ include "csi-common-selectors" . }}
app.kubernetes.io/component: csi-controller
{{- end }}

{{/*
Csi node plugin labels
*/}}
{{- define "csi-node-labels" -}}
{{ include "csi-common-labels" . }}
app.kubernetes.io/component: csi-node
{{- end }}

{{/*
Csi node plugin selector labels
*/}}
{{- define "csi-node-selectors" -}}
{{ include "csi-common-selectors" . }}
app.kubernetes.io/component: csi-node
{{- end }}

{{/*
General labels that should be attached to all objects
*/}}
{{- define "csi-common-labels" -}}
{{ include "csi-common-selectors" . }}
app.kubernetes.io/version: {{ .Chart.AppVersion | quote }}
{{- end }}

{{/*
General selector labels that should be attached to all objects
*/}}
{{- define "csi-common-selectors" -}}
app.kubernetes.io/part-of: {{ include "csi-name" . }}
app.kubernetes.io/name: {{ include "csi-name" . }}
app.kubernetes.io/instance: {{ include "csi-name" . }}
{{- end }}

{{/*
Define service name
*/}}
{{- define "csi-controller-service-name" -}}
{{- default "hostpath-csi-controller" .Values.serviceName }}
{{- end }}

{{/*
Define plugin name
*/}}
{{- define "csi-name" -}}
{{- default "rusa.hostpath.csi.k8s.io" .Values.pluginName | quote }}
{{- end }}