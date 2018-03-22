import { PubSubService } from "../../../services/PubSub";
import { AppRoot } from '../../../services/AppRoot';
import * as feedback from "../../../dto/Web/Feedback";
import Vue from "vue";
import { Component, Watch } from "vue-property-decorator";

interface IFeedback {
    description: string;
    email: string;
    writtenAtUtc: Date;
}

@Component
export default class AnalyzeFeedbackComponent extends Vue {
    private static readonly selectCollectionTitle: string = '(select collection)';

    incidentId: number = 0;
    feedbackList: IFeedback[] = [];

    created() {
        this.incidentId = parseInt(this.$route.params.incidentId, 10);

        var q = new feedback.GetIncidentFeedback();
        q.IncidentId = this.incidentId;
        AppRoot.Instance.apiClient.query<feedback.GetIncidentFeedbackResult>(q)
            .then(result => {
                result.Items.forEach(x => {
                    if (x.Message) {
                        this.feedbackList.push({
                            description: x.Message,
                            email: x.EmailAddress,
                            writtenAtUtc: x.WrittenAtUtc
                        });
                    }
                });
            });
    }
    


}