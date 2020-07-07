<template>
    <div class=" position-relative  mb-2" style="min-height:200px">
        <b-modal :id="cardId">
            <div class="modalBody">
                {{ cardJson }}
            </div>
        </b-modal>
        <loading :isLoading="isLoading" v-bind:color="'primary'" />
        <!-- <div v-if="isError" class="d-flex justify-content-center mt-2 p-2">
            <b-alert
                show
                variant="warning"
                dismissible
                @dismissed=";(isError = false), pic2Card(url)"
            >
                {{ error }} please try again.!
            </b-alert>
        </div> -->
        <div class="d-flex bg-white h-100 w-100">
            <div class=" left-container mr-1 bg-light">
                <b-img-lazy
                    v-if="imageBoundary"
                    v-bind="{
                        blank: true,
                        blankColor: '#bbb',
                        width: 350,
                        height: 350
                    }"
                    :src="imageBoundary | image_data_url"
                    rounded
                ></b-img-lazy>
            </div>
            <div class="right-container ml-1  bg-light p-2">
                <div :id="cardId" class="d-flex justify-content-center"></div>
                <div class="d-flex justify-content-center m-1 -1">
                    <div
                        v-if="cardJson"
                        class="btn btn-sm btn-primary"
                        v-b-modal="cardId"
                    >
                        Card Json
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import Loader from '../../components/loader/Loader.vue'
import imageapi from '@/services/ImageApi.js'
import * as AdaptiveCards from 'adaptivecards'
import AdaptiveCardApi from '../../services/ImageApi'
import Config from '../../utils/config'
import MarkdownIt from 'markdown-it'
export default {
    name: 'RenderImageItem',
    props: {
        url: String,
        id: String
    },
    components: {
        loading: Loader
    },
    data() {
        return {
            isLoading: false,
            imageString: this.url,
            cardHtml: null,
            imageBoundary: null,
            cardJson: null,
            isError: false,
            error: '',
            cardId: 'card' + this.id
        }
    },
    filters: {
        image_data_url(value) {
            return 'data:image/png;base64,' + value
        }
    },
    methods: {
        pic2Card(base64_image) {
            this.isLoading = true
            AdaptiveCardApi.getAdaptiveCard(base64_image)
                .then(response => {
                    let card_json = response.data['card_json']
                    this.cardJson = JSON.stringify(card_json, null, 4)
                    this.imageBoundary = response.data.image || null
                    // Add markdown rendering.
                    AdaptiveCards.AdaptiveCard.onProcessMarkdown = function(
                        text,
                        result
                    ) {
                        let md = new MarkdownIt()
                        result.outputHtml = md.render(text)
                        result.didProcess = true
                    }

                    let adaptiveCard = new AdaptiveCards.AdaptiveCard()
                    adaptiveCard.hostConfig = new AdaptiveCards.HostConfig(
                        Config.adaptiveHostConfig
                    )
                    adaptiveCard.parse(card_json)
                    this.cardHtml = adaptiveCard.render()
                    document
                        .getElementById(this.cardId)
                        .appendChild(this.cardHtml)
                    // Also update the image that has bounding box.
                    this.imageString = response.data['image']
                    this.isLoading = false
                })
                .catch(err => {
                    console.log(err)
                    this.isLoading = false
                    this.error = 'Something Went Wrong'
                    this.isError = true
                })
        }
    },
    mounted() {
        this.pic2Card(this.imageString)
    },
    destroyed() {
        console.log('renderimageitem-destroyed')
    }
}
</script>

<style lang="scss" scoped>
.left-container {
    display: flex;
    flex: 1;
    flex-direction: column;
    justify-content: center;
    align-items: center;
}
.right-container {
    display: flex;
    flex: 1;
    flex-direction: column;
}
.title {
    font-size: 1.2rem;
    font-variant-caps: petite-caps;
    font-weight: bold;
    color: #505050;
    padding: 0.5rem;
}
.modalBody {
    overflow: auto;
}
</style>
