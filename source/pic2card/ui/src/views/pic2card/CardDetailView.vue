<template>
    <div class="d-flex   flex-column position-relative ">
        <loading :isLoading="isLoading" v-bind:color="'primary'" />
        <div v-show="!isLoading" class="d-flex bg-white h-100">
            <div class=" left-container mr-1 bg-light">
                <div v-if="!isLoading" class="title text-center">
                    Picture Boundary
                </div>
                <b-img-lazy
                    v-if="imageBoundary"
                    v-bind="{
                        blank: true,
                        blankColor: '#bbb',
                        width: 450,
                        height: 450
                    }"
                    :src="imageBoundary | image_data_url"
                    rounded
                ></b-img-lazy>
            </div>
            <div class="right-container ml-1  bg-light p-2">
                <div v-if="!isLoading" class="title text-center">
                    Adaptive Card
                </div>
                <div ref="cards" class="d-flex justify-content-center"></div>
                <div class="d-flex justify-content-center">
                    <div
                        v-if="cardJson"
                        class="btn btn-sm btn-primary"
                        v-b-modal.my-modal
                    >
                        Card Json
                    </div>
                    <b-modal id="my-modal" content-class="">{{
                        cardJson
                    }}</b-modal>
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
    name: 'CardDetailView',
    props: {
        url: String
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
            cardJson: null
        }
    },
    computed: {
        image_data_url() {
            return 'data:image/png;base64,' + this.imageString
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
                    // console.log(response.data)
                    let card_json = response.data['card_json']
                    this.cardJson = JSON.stringify(card_json, null, 2)
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
                    this.$refs.cards.appendChild(this.cardHtml)

                    // Also update the image that has bounding box.
                    this.imageString = response.data['image']
                    this.isLoading = false
                })
                .catch(err => {
                    console.log(err)
                    this.isLoading = false
                })
        }
    },
    beforeMount() {
        this.pic2Card(this.imageString)
    },
    mounted() {
        this.$refs.cards.innerHTML = ''
        if (this.cardHtml) {
            this.$refs.cards.appendChild(this.cardHtml)
        }
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
</style>
